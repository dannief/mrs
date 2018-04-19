using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Domain.States;
using MRS.Infrastructure.Aspects;
using System.Data.Entity;

namespace MRS.Infrastructure.EFData
{
    public class RequestRepository : IRequestRepository
    {
        private MrsContext context;

        public RequestRepository(MrsContext context)
        {
            this.context = context;
        }
        
        public Request GetByRequestNumber(string requestNumber, Func<Request, bool> filter = null)
        {
            var requestNumberGuid = new Guid(requestNumber);

            var query =
                GetRequestWithIncludes()
                .Where(x => x.RequestNumber == requestNumberGuid);

            if(filter != null)
            {
                query = query.Where(filter).AsQueryable();
            }

            var request = query.SingleOrDefault();

            if (request != null)
            {
                SetNullsWithNullObjects(request);
            }
            else
            {
                request = Request.None;
            }

            return request;
        }
                
        public ICollection<Request> GetRequests(Func<Request, bool> filter)
        {
            var requests = GetRequestWithIncludes()
                .Where(filter)
                .ToList();

            return requests;
        }        
        
        public void SaveRequest(Request request)
        {
            ReplaceNullObjectsWithNulls(request);

            request.Requester.Tenancies = null;

            if (request.Requester is Supervisor)
            {
                var requester = request.Requester as Supervisor;
                requester.Workers = null;
                requester.SupervisedLocation = null;
                requester.Tenancies = null;
            }
            else if (request.Requester is Worker)
            {
                var requester = request.Requester as Worker;
                requester.Supervisor = null;
                requester.WorkCategories = null;
            }

            context.Set<Location>().Attach(request.LocationToService);
            context.Set<Category>().Attach(request.Category);
            context.Set<User>().Attach(request.Requester);
            context.Set<RequestState>().Attach(request.State);
            if (request.WorkOrder != null)
            {
                var workOrder = request.WorkOrder;
                context.Set<User>().Attach(workOrder.AssignedWorker);

                if (workOrder.WorkOrderNumber == Guid.Empty)
                {
                    workOrder.WorkOrderNumber = Guid.NewGuid();
                    context.Entry(workOrder).State = EntityState.Added;
                }
                else
                {
                    context.Entry(workOrder).State = EntityState.Modified;
                }
            }

            var oldRequest = context.Set<Request>()
            .Include(x => x.State)
            .Where(x => x.RequestNumber == request.RequestNumber)
            .AsNoTracking()
            .SingleOrDefault();

            context.Entry(request).State = oldRequest == null ? EntityState.Added : EntityState.Modified;

            if (oldRequest != null && oldRequest.State != request.State)
            {
                context.ChangeRelationshipState(request, request.State, r => r.State, EntityState.Added);

                oldRequest.State.Request = null;
                context.Set<RequestState>().Attach(oldRequest.State);
                context.ChangeRelationshipState(request, oldRequest.State, r => r.State, EntityState.Deleted);
            }

            context.SaveChanges();
        }
        
        public void UpdateRequestDetails(string requestNumber, string title, string description)
        {
            context.Set<Request>()
                .Where(x => x.RequestNumber == new Guid(requestNumber))
                .Update(x => new Request { Title = title, Description = description });
                      
            context.SaveChanges();
        }

        private IQueryable<Request> GetRequestWithIncludes()
        {
            return context.Set<Request>()
                            .Include(x => x.Category)
                            .Include(x => x.LocationToService)
                            .Include(x => x.Requester)
                            .Include(x => x.State)                            
                            .Include(x => x.WorkOrder.AssignedWorker)                            
                            .Include(x => x.LocationToService.ParentLocation.ParentLocation)
                            .Include(x => x.Category.ParentCategory)
                            .AsNoTracking();
        }

        
        private void SetNullsWithNullObjects(Request request)
        {
            void SetNullLocation(Location location)
            {
                Location tempLocation = location;

                while (tempLocation.ParentLocation != null)
                {
                    tempLocation = tempLocation.ParentLocation;
                }

                tempLocation.ParentLocation = Location.None;
            }

            void SetNullCategory(Category category)
            {
                Category tempCategory = category;

                while (tempCategory.ParentCategory != null)
                {
                    tempCategory = tempCategory.ParentCategory;
                }

                tempCategory.ParentCategory = Category.None;
            }


            SetNullCategory(request.Category);
            SetNullLocation(request.LocationToService);
            if (request.WorkOrder == null) request.WorkOrder = WorkOrder.None;
        }

        private void ReplaceNullObjectsWithNulls(Request request)
        {
            void UnSetNullLocation(Location location)
            {
                Location tempLocation = location;

                while (tempLocation.ParentLocation != null && !tempLocation.ParentLocation.IsNull)
                {
                    tempLocation = tempLocation.ParentLocation;
                }

                tempLocation.ParentLocation = null;
            }

            void UnSetNullCategory(Category category)
            {
                Category tempCategory = category;

                while (tempCategory.ParentCategory != null && !tempCategory.ParentCategory.IsNull)
                {
                    tempCategory = tempCategory.ParentCategory;
                }

                tempCategory.ParentCategory = null;
            }

            UnSetNullCategory(request.Category);
            UnSetNullLocation(request.LocationToService);
            if (request.WorkOrder != null && request.WorkOrder.IsNull) request.WorkOrder = null;
        }
    }
}

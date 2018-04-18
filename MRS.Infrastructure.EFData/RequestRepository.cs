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

        public Request GetByRequestNumber(string requestNumber)
        {
            var request = GetRequestWithIncludes()
                .SingleOrDefault(x => x.RequestNumber == new Guid(requestNumber));

            SetNullCategory(request.Category);
            SetNullLocation(request.LocationToService);

            return request;
        }

        public Request GetByRequestNumber(string requestNumber, Func<Request, bool> filter)
        {
            var request = GetRequestWithIncludes()
                .Where(x => filter(x) && x.RequestNumber == new Guid(requestNumber))
                .SingleOrDefault();

            if (request != null)
            {
                SetNullCategory(request.Category);
                SetNullLocation(request.LocationToService);
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
        
        //[AuditAspect]
        public void SaveRequest(Request request)
        {
            UnSetNullCategory(request.Category);
            UnSetNullLocation(request.LocationToService);
            
            request.Requester.Tenancies = null;

            if (request.Requester is Supervisor)
            {
                var requester = request.Requester as Supervisor;
                requester.Workers = null;
                requester.SupervisedLocation = null;
                requester.Tenancies = null;
            }
            else if(request.Requester is Worker)
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
                context.Entry(workOrder).State = workOrder.WorkOrderNumber == Guid.Empty ? EntityState.Added : EntityState.Modified;
            }
                      
            context.Entry(request).State = request.RequestNumber == Guid.Empty ? EntityState.Added : EntityState.Modified;
                        
            if (request.RequestNumber != Guid.Empty)
            {
                var oldState = context.Set<Request>()
                .Include(x => x.State)
                .Where(x => x.RequestNumber == request.RequestNumber)
                .Select(x => x.State)
                .Single();
                context.ChangeRelationshipState(request, request.State, r => r.State, EntityState.Added);
                context.ChangeRelationshipState(request, oldState, r => r.State, EntityState.Deleted);
            }

            context.SaveChanges();
        }
                       
        //[AuditAspect]
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

        private void SetNullLocation(Location location)
        {
            Location tempLocation = location;

            while (tempLocation.ParentLocation != null)
            {
                tempLocation = tempLocation.ParentLocation;
            }

            tempLocation.ParentLocation = Location.None;
        }

        private void SetNullCategory(Category category)
        {
            Category tempCategory = category;

            while (tempCategory.ParentCategory != null)
            {
                tempCategory = tempCategory.ParentCategory;
            }

            tempCategory.ParentCategory = Category.None;
        }

        private void UnSetNullLocation(Location location)
        {
            Location tempLocation = location;

            while (tempLocation.ParentLocation != null && !tempLocation.ParentLocation.IsNull)
            {
                tempLocation = tempLocation.ParentLocation;
            }

            tempLocation.ParentLocation = null;
        }

        private void UnSetNullCategory(Category category)
        {
            Category tempCategory = category;

            while (tempCategory.ParentCategory != null && !tempCategory.ParentCategory.IsNull)
            {
                tempCategory = tempCategory.ParentCategory;
            }

            tempCategory.ParentCategory = null;
        }
    }
}

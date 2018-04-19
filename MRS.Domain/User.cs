using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain.States;

namespace MRS.Domain
{
    public abstract class User
    {
        public static readonly NullUser None = new NullUser();

        public virtual string IDNumber { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Name { get { return FirstName + " " + LastName; } }
        
        public ICollection<Room> Tenancies { get; set; }


        public virtual bool IsNull { get { return false; } }


        public User()
        {
            this.Tenancies = new List<Room>();           
        }

        public virtual Request CreateRequest(
            Guid requestNumber,
            string title, 
            string description, 
            Location locationToService, 
            Category maintenanceCategory, 
            Severity severity)
        {
            if (locationToService == null || locationToService.IsNull)
            {
                throw new ApplicationException("Request must have a location to service");
            }

            if (maintenanceCategory == null || maintenanceCategory.IsNull)
            {
                throw new ApplicationException("Maintenance category must have a location to service");
            }

            return new Request 
            {
                RequestNumber = requestNumber,
                Title = title,
                Description = description,                
                LocationToService = locationToService, 
                Category = maintenanceCategory,
                RequestDate = DateTime.Now,
                Severity = severity,
                Requester = this
            };
        }

        public virtual void UpdateRequest(
            Request request, 
            string title, 
            string description)
        {
            request.Title = title;
            request.Description = description;
        }
                
        public bool IsTenantOfLocation(Location location)
        {
            return location is Room && Tenancies.Contains(location);
        }

        public virtual bool CanCreateRequestForLocation(Location location)
        {
            return location is Room;
        }

        public virtual bool CanChangeToState(RequestState state)
        {
            var request = state.Request;

            var isTenantOfLocationToService = IsTenantOfLocation(request.LocationToService);
            var isNotRequester = request.Requester != this;
            var shouldApproveOrReject = isTenantOfLocationToService && isNotRequester;

            return
                state == request.newState ||
                (state == request.approvedState && shouldApproveOrReject) ||
                (state == request.rejectedState && shouldApproveOrReject);
        }

        public virtual bool CanViewRequest(Request request)
        {
            return request.Requester == this;
        }

        public virtual bool CanViewWorkOrder(Request request)
        {
            return request.WorkOrder != null && request.WorkOrder.AssignedWorker == this;
        }

        public override int GetHashCode()
        {
            return this.IDNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User))
                return false;

            var userObj = (User)obj;

            return userObj.IDNumber == this.IDNumber;
        }

        public static bool operator ==(User obj1, User obj2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)obj1 == null) || ((object)obj2 == null))
            {
                return false;
            }

            return obj1.IDNumber == obj2.IDNumber;
        }

        public static bool operator !=(User obj1, User obj2)
        {
            return !(obj1 == obj2);
        }

        public class NullUser : User
        {
            public override string IDNumber { get { return string.Empty; } }
            public override string EmailAddress { get { return string.Empty; } }
            public override string FirstName { get { return string.Empty; } }
            public override string LastName { get { return string.Empty; } }
            public override string Name { get { return string.Empty; } }
            public override bool IsNull { get { return true; } }

            public override string ToString()
            {
                return "No User";
            }
        }        
    }
}

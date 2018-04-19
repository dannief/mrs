using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain.States;

namespace MRS.Domain
{
    public class Request
    {
        public static readonly NullRequest None = new NullRequest();

        // States
        internal RequestState newState;

        internal RequestState approvedState;
        
        internal RequestState rejectedState;
        
        internal RequestState workAssignedState;        
        
        internal RequestState workStartedState;
        
        internal RequestState workRejectedState;
        
        internal RequestState completedState;



        public virtual Guid RequestNumber { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime RequestDate { get; set; }

        public virtual Category Category { get; set; } = Category.None;

        public virtual User Requester { get; set; } = User.None;

        public virtual Location LocationToService { get; set; } = Location.None;

        public virtual Severity Severity { get; set; }

        public virtual WorkOrder WorkOrder { get; set; } = WorkOrder.None;

        

        public virtual bool IsNull { get { return false; } }

        private RequestState state;
        public RequestState State 
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if(state != null)
                    state.Request = this;
            }
        }

        public Request()
        {
            newState = new NewState(this);
            workAssignedState = new WorkAssignedState(this);
            rejectedState = new RejectedState(this);
            workAssignedState = new WorkAssignedState(this);
            completedState = new CompletedState(this);
            workStartedState = new WorkStartedState(this);
            approvedState = new ApprovedState(this);
            workRejectedState = new WorkRejectedState(this);

            State = newState;
        }

        public void AddWorkOrder(WorkOrder workOrder)
        {
            WorkOrder = workOrder;
            // Transtion state
            State.CreateWorkOrder();
        }

        public void StartWork(Worker worker)
        {
            // Transtion state
            State.StartWork();
        }

        public void CompleteWork(Worker worker)
        {
            // Transtion state
            State.CompleteWork();
        }

        public void RejectWork(Worker worker, string reason)
        {
            // Transtion state
            State.RejectWork();
        }

        public void RejectRequest(User user)
        {
            // Transtion state
            State.RejectRequest();
        }

        public void ApproveRequest(User user)
        {
            // Transition State
            State.ApproveRequest();
        }

        public override string ToString()
        {
            return RequestNumber + ": " + Title;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Request))
                return false;

            var requestToCompare = obj as Request;

            return requestToCompare == this;
        }

        public override int GetHashCode()
        {
            return this.RequestNumber.GetHashCode();
        }

        public static bool operator ==(Request obj1, Request obj2)
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

            return obj1.RequestNumber == obj2.RequestNumber;
        }

        public static bool operator !=(Request obj1, Request obj2)
        {
            return !(obj1 == obj2);
        }

        public class NullRequest : Request
        {
            public override string Title { get { return string.Empty; } }
            public override string Description { get { return string.Empty; } }
            public override Category Category { get { return Category.None; } }
            public override Location LocationToService { get { return Location.None; } }
            public override bool IsNull { get { return true; } }

            public override string ToString()
            {
                return "No Request";
            }
        }
    }
}

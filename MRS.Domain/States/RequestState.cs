using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain.Utils;

namespace MRS.Domain.States
{
    public abstract class RequestState : IRequestState
    {
        public virtual Request Request { get; set; }

        public virtual string ID { get; set; }

        public virtual string Name { get; set; }

        public virtual bool IsNull { get { return false; } }

        public RequestState()
        {
        
        }

        public RequestState(Request request)
        {
            this.Request = request;
        }

        public virtual void CreateNewRequest()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void CreateWorkOrder()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void RejectRequest()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void StartWork()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void ApproveRequest()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void RejectWork()
        {
            ThrowInvalidStateTransitionException();
        }

        public virtual void CompleteWork()
        {
            ThrowInvalidStateTransitionException();
        }

        private static void ThrowInvalidStateTransitionException()
        {
            throw new MrsException("This state transition is not valid");
        }

        public abstract ICollection<IRequestState> GetNextPossibleStates();

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is RejectedState))
                return false;

            var stateObj = (RequestState)obj;

            return stateObj.ID == this.ID;
        }

        public static bool operator ==(RequestState obj1, RequestState obj2)
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

            return obj1.ID == obj2.ID;
        }

        public static bool operator !=(RequestState obj1, RequestState obj2)
        {
            return !(obj1 == obj2);
        }

        public class NullRequestState : RequestState
        {
            public override string ID { get { return string.Empty; } }

            public override string Name { get { return string.Empty; } }

            public override Request Request { get { return Request.None; } }

            public override bool IsNull { get { return true; } }

            public override ICollection<IRequestState> GetNextPossibleStates()
            {
                return new List<NullRequestState>().Cast<IRequestState>().ToList();
            }
         
            public override string ToString()
            {
                return "No Request State";
            }
        }
    }
}

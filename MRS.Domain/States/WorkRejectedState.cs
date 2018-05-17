using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class WorkRejectedState : RequestState
    {
        public WorkRejectedState()
            : this(Request.None)
        {
        }

        public WorkRejectedState(Request request) : 
            base("WorkRejected", "Work Rejected", request)
        {
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

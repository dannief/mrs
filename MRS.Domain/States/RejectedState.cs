using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class RejectedState : RequestState
    {
        public RejectedState()
            : this(Request.None)
        {
        }

        public RejectedState(Request request) : 
            base("Rejected", "Rejected", request)
        {
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

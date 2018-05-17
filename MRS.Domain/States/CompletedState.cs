using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class CompletedState : RequestState
    {
        public CompletedState()
            : this(Request.None)
        {
        }

        public CompletedState(Request request) 
            : base ("Completed", "Completed", request)
        {
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

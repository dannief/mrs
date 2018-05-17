using MRS.Domain.Utils;
using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class WorkStartedState : RequestState
    {
        public WorkStartedState()
            : this(Request.None)
        {
        }

        public WorkStartedState(Request request) : 
            base("WorkStarted", "Work Started", request)
        {           
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.Completed());
            states.Add(Request.WorkRejected());

            return states;
        }

        public override void CompleteWork()
        {
            Request.State = Request.Completed();
        }

        public override void RejectRequest()
        {
            Request.State = Request.WorkRejected();
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class WorkStartedState : RequestState
    {
        public WorkStartedState()
            : base()
        {

        }

        public WorkStartedState(Request request) : base(request)
        {
            ID = "WorkStarted";
            Name = "Work Started";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.completedState);
            states.Add(Request.workRejectedState);

            return states;
        }

        public override void CompleteWork()
        {
            Request.State = Request.completedState;
        }

        public override void RejectRequest()
        {
            Request.State = Request.workRejectedState;
        }        
    }
}

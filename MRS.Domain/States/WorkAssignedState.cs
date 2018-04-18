using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class WorkAssignedState : RequestState
    {
        public WorkAssignedState()
            : base()
        {

        }

        public WorkAssignedState(Request request) : base(request)
        {
            ID = "WorkAssigned";
            Name = "Work Assigned";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.workStartedState);
            states.Add(Request.workRejectedState);
            
            return states;
        }

        public override void RejectRequest()
        {
            Request.State = Request.rejectedState;
        }

        public override void StartWork()
        {
            Request.State = Request.workStartedState;
        }
    }
}

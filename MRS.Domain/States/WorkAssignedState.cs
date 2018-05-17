using MRS.Domain.Utils;
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
            : this(Request.None)
        {
        }

        public WorkAssignedState(Request request) 
            : base("WorkAssigned", "Work Assigned", request)
        {
        }
        
        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.WorkStarted());
            states.Add(Request.WorkRejected());
            
            return states;
        }

        public override void RejectWork()
        {
            Request.State = Request.WorkRejected();
        }

        public override void StartWork()
        {
            Request.State = Request.WorkRejected();
        }
    }
}

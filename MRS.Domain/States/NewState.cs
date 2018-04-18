using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class NewState : RequestState
    {
        public NewState()
            : base()
        {

        }

        public NewState(Request request) : base(request)
        {
            ID = "New";
            Name = "New";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.approvedState);
            states.Add(Request.rejectedState);
            states.Add(Request.workAssignedState);

            return states;
        }

        public override void ApproveRequest()
        {
            Request.State = Request.approvedState;
        }

        public override void RejectRequest()
        {
            Request.State = Request.rejectedState;
        }

        public override void CreateWorkOrder()
        {
            Request.State = Request.workAssignedState;
        }
    }
}

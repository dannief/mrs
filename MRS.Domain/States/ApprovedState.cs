using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class ApprovedState : RequestState
    {
        public ApprovedState() : base()
        {

        }

        public ApprovedState(Request request) : base (request) 
        {
            ID = "Approved";
            Name = "Approved";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.rejectedState);
            states.Add(Request.workAssignedState);

            return states;
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

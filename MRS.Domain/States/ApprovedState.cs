using MRS.Domain.Utils;
using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class ApprovedState : RequestState
    {    
        public ApprovedState() 
            : this(Request.None)
        {
        }

        public ApprovedState(Request request) 
            : base("Approved", "Approved", request) 
        {            
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.Rejected());
            states.Add(Request.WorkAssigned());

            return states;
        }

        public override void RejectRequest()
        {
            Request.State = Request.Rejected();
        }

        public override void CreateWorkOrder()
        {
            Request.State = Request.WorkAssigned();
        }
    }
}

using MRS.Domain.Utils;
using System.Collections.Generic;

namespace MRS.Domain.States
{
    public class NewState : RequestState
    {
        public NewState()
            : this(Request.None)
        {
        }

        public NewState(Request request) 
            : base("New", "New" , request)
        {
        }        

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            var states = new List<IRequestState>();
            states.Add(Request.Approved());
            states.Add(Request.Rejected());
            states.Add(Request.WorkAssigned());

            return states;
        }

        public override void ApproveRequest()
        {
            Request.State = Request.Approved();
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

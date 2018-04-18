using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class WorkRejectedState : RequestState
    {
        public WorkRejectedState()
            : base()
        {

        }

        public WorkRejectedState(Request request) : base(request)
        {
            ID = "WorkRejected";
            Name = "Work Rejected";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

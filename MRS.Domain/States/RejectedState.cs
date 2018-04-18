using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class RejectedState : RequestState
    {
        public RejectedState()
            : base()
        {

        }

        public RejectedState(Request request) : base(request)
        {
            ID = "Rejected";
            Name = "Rejected";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

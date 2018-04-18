using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public class CompletedState : RequestState
    {
        public CompletedState()
            : base()
        {

        }

        public CompletedState(Request request) : base (request)
        {
            ID = "Completed";
            Name = "Completed";
        }

        public override ICollection<IRequestState> GetNextPossibleStates()
        {
            return new List<IRequestState>();
        }
    }
}

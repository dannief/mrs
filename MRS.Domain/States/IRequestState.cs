using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.States
{
    public interface IRequestState
    {
        string Name { get; set; }

        ICollection<IRequestState> GetNextPossibleStates();

        void CreateNewRequest();
        void CreateWorkOrder();
        void ApproveRequest();
        void RejectRequest();
        void StartWork();
        void RejectWork();
        void CompleteWork();
    }
}

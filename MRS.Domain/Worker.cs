using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    public class Worker : Staff
    {
        public ICollection<Category> WorkCategories { get; set; }
        public Supervisor Supervisor { get; set; }

        public Worker()
        {
            this.WorkCategories = new List<Category>();
        }

        public override bool CanChangeToState(States.RequestState state)
        {
            var request = state.Request;
            var isAssignedToRequest = IsWorkerAssignedToRequest(request);

            return base.CanChangeToState(state) ||
                (state == request.workStartedState && isAssignedToRequest) ||
                (state == request.workRejectedState && isAssignedToRequest) ||
                (state == request.completedState && isAssignedToRequest);
        }

        public override bool CanViewRequest(Request request)
        {
            return base.CanViewRequest(request) || IsWorkerAssignedToRequest(request);
        }
                
        public bool IsWorkerAssignedToRequest(Request request)
        {
            return request.WorkOrder != null && request.WorkOrder.AssignedWorker == this;
        }
    }
}

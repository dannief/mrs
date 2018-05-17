using MRS.Domain.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    public class Supervisor : Staff
    {
        public Location SupervisedLocation { get; set; }
        public ICollection<Worker> Workers { get; set; }

        public Supervisor()
        {
            Workers = new List<Worker>();
        }

        public override bool CanChangeToState(States.RequestState state)
        {
            var request = state.Request;
            var isSupervisorOfLocationToService = IsSupervisorOfLocation(request.LocationToService);

            return base.CanChangeToState(state) ||
                (state == new WorkAssignedState() && isSupervisorOfLocationToService) ||
                (state == new WorkStartedState() && isSupervisorOfLocationToService) ||
                (state == new WorkRejectedState() && isSupervisorOfLocationToService) ||
                (state == new CompletedState() && isSupervisorOfLocationToService);
        }

        public void CreateWorkOrder(Request request, Worker assignedWorker, string description, Priority priority)
        {
            if (request == null)
            {
                throw new ApplicationException("Work Order must be attached to request");
            }

            if (assignedWorker == null)
            {
                throw new ApplicationException("A Work Order must be assigned to a Worker");
            }

            // Assigned Worker must but be one of the supervisor's workers
            if (!IsWorkerSubordinate(assignedWorker))
            {
                throw new ApplicationException("The assigned worker is not a subordinate of this supervisor");
            }

            var workOrder = new WorkOrder
            {
                AssignedWorker = assignedWorker,
                Description = description,
                Priority = priority
            };

            request.AddWorkOrder(workOrder);
        }

        public bool IsWorkerSubordinate(Worker worker)
        {
            return Workers.Contains(worker);
        }

        public bool IsSupervisorOfLocation(Location location)
        {
            if (location == null || location.IsNull)
                return false;

            return SupervisedLocation == location || IsSupervisorOfLocation(location.ParentLocation);
        }

        public override bool CanViewRequest(Request request)
        {
            return base.CanViewRequest(request) || IsSupervisorOfLocation(request.LocationToService);
        }

        public override bool CanViewWorkOrder(Request request)
        {
            return base.CanViewRequest(request) || IsSupervisorOfLocation(request.LocationToService);
        }
    }
}

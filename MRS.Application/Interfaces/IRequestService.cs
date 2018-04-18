using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.DataTransferObjects;
using MRS.Domain;

namespace MRS.Application.Interfaces
{
    public interface IRequestService
    {
        UserDto AuthenticateUser(string userIdNumber, string password);        
        void UpdateRequestDetails(UpdateRequestDto requestUpdateData);
        RequestDetailsDto GetRequest(string requestNumber, string userIDNumber);
        ICollection<RequestListItemDto> GetRequests(string userIdNumber);
        string CreateWorkOrder(string supervisorIdNumber, string requestNumber, WorkOrderDto workOrderData);
        void UpdateWorkOrder(string requestNumber, WorkOrderDto workOrderData);
        void StartWork(string requestId, string workerIdNumber);
        void CompleteWork(string requestId, string workerIdNumber);
        void RejectWork(string requestId, string workerIdNumber, string reason);
        void RejectRequest(string requestId, string userIdNumber, string reason);
        void ApproveRequest(string requestId, string userIdNumber);
    }
}

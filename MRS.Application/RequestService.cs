using System;
using System.Collections.Generic;
using System.Linq;
using MRS.Application.Interfaces;
using MRS.Application.Mappers;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;
using MRS.Domain.States;
using MRS.Domain.Services;

namespace MRS.Application
{
    public class RequestService : IRequestService
    {
        private IRequestRepository requests;
        private IUserRepository users;
        private ILookupRepository lookupData;

        private AuthenticationService authSvc;
        private UserRequestsService userRequestsSvc;

        public RequestService(IRequestRepository requests, IUserRepository users, ILookupRepository lookupData)
        {
            this.requests = requests;
            this.users = users;
            this.lookupData = lookupData;

            authSvc = new AuthenticationService(users);
            userRequestsSvc = new UserRequestsService(users, requests);
        }

        public void UpdateRequestDetails(UpdateRequestDto requestUpdateData)
        {
            var request = requests.GetByRequestNumber(requestUpdateData.RequestNumber);
            var user = users.GetByIDNumber(requestUpdateData.UserIDNumber);

            user.UpdateRequest(request, requestUpdateData.Title, requestUpdateData.Description);

            requests.SaveRequest(request);
        }
                
        public RequestDetailsDto GetRequest(string requestNumber, string userIDNumber)
        {
            var user = users.GetByIDNumber(userIDNumber);

            var request = userRequestsSvc.GetRequest(requestNumber, user);
            
            return new RequestDtoMapper().FromRequest(request);
        }

        public ICollection<RequestListItemDto> GetRequests(string userIdNumber)
        {
            var user = users.GetByIDNumber(userIdNumber);
            
            var requestList = userRequestsSvc.GetRequestsViewableByUser(user);
            
            var dtoList = requestList.Select(x => new RequestListItemDtoMapper().FromRequest(x)).ToList();
            return dtoList;
        }

        public UserDto AuthenticateUser(string userIdNumber, string password)
        {            
            var user = authSvc.AuthenticateUser(userIdNumber, password);
            
            UserDto userDto = null;
            if (user != null)
            {
                userDto = new UserDto { IDNumber = user.IDNumber, Name = user.FirstName + " " + user.LastName, Type = user.GetType().Name };
            }
            return userDto;
        }
                
        public string CreateWorkOrder(string supervisorIdNumber, string requestNumber, WorkOrderDto workOrderData)
        {
            var supervisor = users.GetByIDNumber(supervisorIdNumber) as Supervisor;
            var worker = users.GetByIDNumber(workOrderData.WorkerIDNumber) as Worker;
            var request = requests.GetByRequestNumber(requestNumber);
            var priority = (Priority)int.Parse(workOrderData.PriorityID);
            
            supervisor.CreateWorkOrder(request, worker, workOrderData.Description, priority);
            requests.SaveRequest(request);
            
            return request.WorkOrder.WorkOrderNumber.ToString();
        }

        public void UpdateWorkOrder(string requestNumber, WorkOrderDto workOrderData)
        {
            var request = requests.GetByRequestNumber(requestNumber);            
            
            // TODO: Make domain call
            new WorkOrderDtoMapper(users).FromDto(request.WorkOrder, workOrderData);
                        
            requests.SaveRequest(request);
        }

        public void StartWork(string requestNumber, string workerIdNumber)
        {
            var request = requests.GetByRequestNumber(requestNumber);
            var worker = users.GetByIDNumber(workerIdNumber) as Worker;

            // TODO: Make domain call
            request.StartWork(worker);
            
            requests.SaveRequest(request);
        }

        public void CompleteWork(string requestNumber, string workerIdNumber)
        {
            var request = requests.GetByRequestNumber(requestNumber);
            var worker = users.GetByIDNumber(workerIdNumber) as Worker;
            
            // TODO only users can move states.
            // Add to domain service
            // Log who changed state
            request.CompleteWork(worker);
            
            requests.SaveRequest(request);
        }

        public void RejectWork(string requestNumber, string workerIdNumber, string reason)
        {
            var request = requests.GetByRequestNumber(requestNumber);
            var worker = users.GetByIDNumber(workerIdNumber) as Worker;

            // TODO: Make domain call
            request.RejectWork(worker, reason);
            
            requests.SaveRequest(request);
        }

        public void RejectRequest(string requestNumber, string userIdNumber, string reason)
        {
            var request = requests.GetByRequestNumber(requestNumber);
            var user = users.GetByIDNumber(userIdNumber);

            // TODO: Make domain call
            request.RejectRequest(user);
            
            requests.SaveRequest(request);
        }

        public void ApproveRequest(string requestNumber, string userIdNumber)
        {
            var request = requests.GetByRequestNumber(requestNumber);
            var user = users.GetByIDNumber(userIdNumber);

            // TODO: Make domain call
            request.ApproveRequest(user);
            
            requests.SaveRequest(request);
        }
    }
}
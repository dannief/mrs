using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.DataTransferObjects;
using MRS.Domain;
using MRS.Domain.Interfaces;

namespace MRS.Application.Mappers
{
    public class RequestListItemDtoMapper
    {       
        public RequestListItemDto FromRequest(Request request)
        {
            var dto = new RequestListItemDto
            {                
                RequestNumber = request.RequestNumber.ToString(),
                Title = request.Title,
                Location = request.LocationToService.ToString(),
                Requester = request.Requester.Name,
                Serverity = request.Severity.ToString(),
                Category = request.Category.Name,
                State = request.State.Name
            };

            var workOrder = request.WorkOrder;
            if (workOrder != null)
            {
                var workOrderListItemDto = new WorkOrderListItemDto
                {
                    WorkOrderNumber = workOrder.WorkOrderNumber.ToString(),
                    Description = workOrder.Description,
                    Priority = workOrder.Priority.ToString(),
                    Worker = workOrder.AssignedWorker.Name,
                    RequestNumber = dto.RequestNumber
                };

                dto.WorderOrderItem = workOrderListItemDto;
            }

            return dto;
        }
    }
}

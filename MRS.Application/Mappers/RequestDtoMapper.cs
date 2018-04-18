using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.DataTransferObjects;
using MRS.Domain;

namespace MRS.Application.Mappers
{
    public class RequestDtoMapper
    {        
        public RequestDtoMapper()
        {

        }

        public RequestDetailsDto FromRequest(Request request)
        {
            var dto = new RequestDetailsDto
            {
                Description = request.Description,
                LocationID = request.LocationToService.ID,
                MainCategoryID = request.Category.ParentCategory.ID,
                SubCategoryID = request.Category.ID,
                RequesterIDNumber = request.Requester.IDNumber,
                RequestNumber = request.RequestNumber.ToString(),
                Title = request.Title,
                SeverityID = ((int)request.Severity).ToString(),
                LocationName = request.LocationToService.ToString(),
                State = request.State.Name
            };

            
            var workOrder = request.WorkOrder;
            if (workOrder.IsNull)
            {
                var workOrderDto = new WorkOrderDto
                {
                    WorkOrderNumber = workOrder.WorkOrderNumber.ToString(),
                    WorkerIDNumber = workOrder.AssignedWorker.IDNumber,
                    PriorityID = ((int)workOrder.Priority).ToString(),
                    Description = workOrder.Description
                };

                dto.WorderOrderData = workOrderDto;
            }
            
            return dto;
        }
    }
}

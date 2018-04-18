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
    public class WorkOrderDtoMapper
    {
        private IUserRepository users;

        public WorkOrderDtoMapper(IUserRepository users)
        {
            this.users = users;
        }

        public WorkOrder FromDto(WorkOrder wo, WorkOrderDto dto)
        {            
            wo.AssignedWorker = users.GetByIDNumber(dto.WorkerIDNumber) as Worker;
            wo.Description = dto.Description;
            wo.Priority = (Priority)int.Parse(dto.PriorityID);
            wo.WorkOrderNumber = new Guid(dto.WorkOrderNumber);
           
            return wo;
        }
    }
}

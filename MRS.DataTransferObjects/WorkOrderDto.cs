using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class WorkOrderDto
    {
        public string WorkOrderNumber { get; set; }
        public string WorkerIDNumber { get; set; }
        public string Description { get; set; }
        public string PriorityID { get; set; }        
    }
}

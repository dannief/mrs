using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class WorkOrderListItemDto
    {
        public string WorkOrderNumber { get; set; }
        public string Worker { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string RequestNumber { get; set; }
    }
}

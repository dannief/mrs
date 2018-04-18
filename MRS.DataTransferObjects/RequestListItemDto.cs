using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class RequestListItemDto
    {        
        public string RequestNumber { get; set; }
        public string Requester { get; set; }
        public string Title { get; set; }        
        public string Location { get; set; }
        public string Category { get; set; }        
        public string Serverity { get; set; }
        public string State { get; set; }
        public WorkOrderListItemDto WorderOrderItem { get; set; }
    }
}

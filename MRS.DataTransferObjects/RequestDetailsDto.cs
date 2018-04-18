using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class RequestDetailsDto
    {
        public string RequestNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public string MainCategoryID { get; set; }
        public string SubCategoryID { get; set; }
        public string RequesterIDNumber { get; set; }
        public string SeverityID { get; set; }
        public string State { get; set; }
        public WorkOrderDto WorderOrderData { get; set; }
    }
}

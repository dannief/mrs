using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class CreateRequestDto
    {        
        public string Title { get; set; }
        public string Description { get; set; }
        public string LocationID { get; set; }       
        public string SubCategoryID { get; set; }
        public string RequesterIDNumber { get; set; }
        public string SeverityID { get; set; }
    }
}

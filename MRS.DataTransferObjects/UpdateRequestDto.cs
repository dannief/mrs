using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class UpdateRequestDto
    {
        public string RequestNumber { get; set; }
        public string UserIDNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
    }
}

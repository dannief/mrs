using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.DataTransferObjects
{
    public class ChangeTrackingDto
    {
        public DtoState State { get; set; }

        public ChangeTrackingDto()
        {
            State = DtoState.New;
        }
    }
}

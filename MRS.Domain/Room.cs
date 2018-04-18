using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    public class Room : Location
    {
        public Room() : base()
        {

        }

        public Room(string Id, string name, Location parentLocation = null) 
            : base(Id, name, parentLocation)
        {

        }
    }
}

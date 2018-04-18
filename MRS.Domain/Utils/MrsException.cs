using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.Utils
{
    public class MrsException : Exception
    {
        public MrsException(string message) : base(message) { }
    }
}

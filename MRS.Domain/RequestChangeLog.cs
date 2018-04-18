using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestChangeLog
    {
        public int ID { get; set; }
        public string ChangeID { get; set; }
        public string RequestNumber { get; set; }
        public string UserIDNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string Value { get; set; }
    }
}

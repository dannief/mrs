using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.Interfaces
{
    public interface IRequestRepository
    {
        Request GetByRequestNumber(string requestNumber);
        Request GetByRequestNumber(string requestNumber, Func<Request, bool> filter);
        ICollection<Request> GetRequests(Func<Request,bool> filter);        
        void SaveRequest(Request request);        
    }
}

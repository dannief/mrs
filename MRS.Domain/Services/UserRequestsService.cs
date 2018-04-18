using MRS.Domain.Interfaces;
using MRS.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.Services
{
    public class UserRequestsService
    {
        private IUserRepository users;
        private IRequestRepository requests;

        public UserRequestsService(IUserRepository users, IRequestRepository requests)
        {
            this.users = users;
            this.requests = requests;
        }

        public ICollection<Request> GetRequestsViewableByUser(User user)
        {
            var requestList = requests.GetRequests(user.CanViewRequest);
            requestList.ToList().ForEach(r => 
            {
                if (!user.CanViewWorkOrder(r))
                {
                    r.WorkOrder = WorkOrder.None;
                }
            });
            return requestList;
        }

        public Request GetRequest(string requestNumber, User user)
        {
            var request = requests.GetByRequestNumber(requestNumber, user.CanViewRequest);

            if(request.IsNull)
            {
                throw new MrsException("The user does not have access to the request");
            }

            if (!user.CanViewWorkOrder(request))
            {
                request.WorkOrder = WorkOrder.None;
            }

            return request;
        }
    }
}

using MRS.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain
{
    public class Student : User
    {
        public override Request CreateRequest(
            Guid requestNumber,
            string title, 
            string description, 
            Location locationToService, 
            Category maintenanceCategory, 
            Severity severity)
        {
            CheckIfUserIsTenantOfLocation(locationToService);

            var request = base.CreateRequest(requestNumber, title, description, locationToService, maintenanceCategory, severity);
                       
            return request;
        }

        public override void UpdateRequest(Request request, string title, string description)
        {
            CheckIfUserIsTenantOfLocation(request.LocationToService);

            base.UpdateRequest(request, title, description);
        }

        public override bool CanCreateRequestForLocation(Location location)
        {
            return IsTenantOfLocation(location);
        }

        private void CheckIfUserIsTenantOfLocation(Location locationToService)
        {
            if (!IsTenantOfLocation(locationToService))
                throw new MrsException("A student cannot create a request for a room where she does not reside");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using MRS.Website.Code;
using MRS.Application.Interfaces;
using System.Web.Security;
using MRS.DataTransferObjects;


namespace MRS.Website.Code
{
    public class UserPrincipal : GenericPrincipal
    {
        private IRequestService requestService;

        public string IDNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public UserPrincipal(FormsIdentity identity, IRequestService requestService)
            : base(identity, null)         
        {
            this.requestService = requestService;
            var userData = identity.Ticket.UserData.Deserialize<UserDto>();
            IDNumber = userData.IDNumber;
            Name = userData.Name;
            Type = userData.Type;
        }

        public override bool IsInRole(string role)
        {            
            return true;
        }        
    }
}
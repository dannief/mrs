using MRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.Services
{
    public class AuthenticationService
    {
        private IUserRepository users;

        public AuthenticationService(IUserRepository users)
        {
            this.users = users;
        }

        public User AuthenticateUser(string idNumber, string password)
        {
            return users.GetByIDNumberAndPassword(idNumber, password);
        }
    }
}

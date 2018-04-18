using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRS.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetByIDNumberAndPassword(string idNumber, string password);
        User GetByIDNumber(string idNumber);
        Supervisor GetSupervisorForLocation(Location location);
        ICollection<Worker> GetWorkersForLocation(Location location);
    }
}

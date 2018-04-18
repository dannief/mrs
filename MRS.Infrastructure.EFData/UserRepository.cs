using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using MRS.Domain.Interfaces;

namespace MRS.Infrastructure.EFData
{
    public class UserRepository : IUserRepository
    {
         private MrsContext context;

         public UserRepository(MrsContext context)
        {
            this.context = context;
        }

        public User GetByIDNumber(string idNumber)
        {

            User user;

            user = context.Set<Supervisor>()
                .Include(x => x.Tenancies)
                .Include(x => x.SupervisedLocation)
                .Include(x => x.Workers)
                .AsNoTracking()
                .SingleOrDefault(x => x.IDNumber == idNumber); ;

            if (user == null)
            {
                user = context.Set<Worker>()
                    .Include(x => x.Tenancies)
                    .Include(x => x.Supervisor)
                    .AsNoTracking()
                    .SingleOrDefault(x => x.IDNumber == idNumber); ;

                if (user == null)
                {
                    user = context.Set<User>()
                        .Include(x => x.Tenancies)
                        .Where(x => !(x is Worker) && !(x is Supervisor))
                        .AsNoTracking()
                        .SingleOrDefault(x => x.IDNumber == idNumber);
                }
            }
        
                        
            return user;
        }

        public User GetByIDNumberAndPassword(string idNumber, string password)
        {
            return GetByIDNumber(idNumber);
        }

        public Supervisor GetSupervisorForLocation(Location location)
        {
            var locationIDs = location.Flatten().Select(x => x.ID);
            var supervisor = context.Set<Supervisor>()
                .Include(x => x.SupervisedLocation)
                .Include(x => x.Workers)
                .AsNoTracking()
                .SingleOrDefault(x => locationIDs.Contains(x.SupervisedLocation.ID));

            return supervisor;
        }

        public ICollection<Worker> GetWorkersForLocation(Location location)
        {
            var workers = new List<Worker>();
            var supervisor = GetSupervisorForLocation(location);
            if (supervisor != null)
                workers = supervisor.Workers.ToList();
            return workers;
        }
    }
}

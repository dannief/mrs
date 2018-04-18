using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Domain.Interfaces
{
    public interface ILookupRepository
    {
        ICollection<Location> GetLocations();
        ICollection<Category> GetCategories();
        ICollection<Worker> GetWorkers();
    }
}

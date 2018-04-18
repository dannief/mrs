using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain;
using MRS.Domain.Interfaces;

namespace MRS.Infrastructure.EFData
{
    public class LookupRepository : ILookupRepository
    {
        private MrsContext context;

        public LookupRepository(MrsContext context)
        {
            this.context = context;
        }

        public ICollection<Location> GetLocations()
        {
            var locations = context.Set<Location>().AsNoTracking()
                .Include("ParentLocation")
                .Include("ParentLocation.ParentLocation")
                .ToList();
            locations.ForEach(x => 
            {
                if (x.ParentLocation == null)
                {
                    x.ParentLocation = Location.None;
                }
            });
            return locations;
        }

        public ICollection<Category> GetCategories()
        {
            var categories = context.Set<Category>().AsNoTracking()
                .Include("ParentCategory").Include("SubCategories").ToList();
            categories.ForEach(x =>
            {
                if (x.ParentCategory == null)
                {
                    x.ParentCategory = Category.None;
                }
            });
            return categories;
        }
        
        public ICollection<Worker> GetWorkers()
        {
            var workers = context.Set<Worker>().AsNoTracking().ToList();
            return workers;
        }
    }
}

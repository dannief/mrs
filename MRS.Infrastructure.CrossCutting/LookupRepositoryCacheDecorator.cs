using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain.Interfaces;
using MRS.Infrastructure.Common.Cache;

namespace MRS.Infrastructure.CrossCutting
{
    /// <summary>
    /// Decorator class for ILookupRepository that enables caching of return values
    /// </summary>
    public class LookupRepositoryCacheDecorator : ILookupRepository
    {
        private ILookupRepository repository;
        private ICacheProvider cache;


        /// <summary>
        /// Initializes a new instance of the <see cref="LookupRepositoryCacheDecorator"/> class.
        /// Initializies the class dependencies on the ILookupRepository and the ICacheProvider
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cache">The cache.</param>
        public LookupRepositoryCacheDecorator(ILookupRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        /// <summary>
        /// Returns the value from the cache if it exists, otherwise the ILookupRepository method 
        /// is called to return the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getObject"></param>
        /// <param name="expirationHrs"></param>
        /// <returns></returns>
        private ICollection<T> GetObject<T>(Func<ICollection<T>> getObject, double expirationHrs = 8)
        {
            ICollection<T> obj = new List<T>();
            string cacheKey = GetCacheKey(typeof(T));

            obj = cache.Get<ICollection<T>>(cacheKey);

            if (obj == null)
            {
                obj = getObject();
                if (obj != null)
                {
                    if (expirationHrs == 0)
                        cache.Add(cacheKey, obj);
                    else
                        cache.Add(cacheKey, obj, DateTime.Now.AddHours(expirationHrs));
                }
            }

            return obj;
        }

        /// <summary>
        /// Creates a cache key based on the type that is returned
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetCacheKey(Type type)
        {
            return type.Name + "_LookupValues";
        }

        /// <summary>
        /// Gets the locations from the cache or from the ILookupRepository instance if it does not exist
        /// </summary>
        /// <returns></returns>
        public ICollection<Domain.Location> GetLocations()
        {
            return GetObject(() => repository.GetLocations());
        }

        /// <summary>
        /// Gets the categories from the cache or from the ILookupRepository instance if it does not exist
        /// </summary>
        /// <returns></returns>
        public ICollection<Domain.Category> GetCategories()
        {
            return GetObject(() => repository.GetCategories());
        }
        
        public ICollection<Domain.Worker> GetWorkers()
        {
            return GetObject(() => repository.GetWorkers());
        }
    }
}

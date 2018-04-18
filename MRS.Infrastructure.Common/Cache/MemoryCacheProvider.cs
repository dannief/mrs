using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Data.SqlClient;

namespace MRS.Infrastructure.Common.Cache
{
    [Serializable]
    public class MemoryCacheProvider : ICacheProvider
    {        
        public T Get<T>(string key)
        {
            T item = default(T);
            if (MemoryCache.Default[key] != null)
            {
                item = (T)MemoryCache.Default[key];
            }
            return item;
        }
        
        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void Add(string key, object data)
        {
            MemoryCache.Default.Add(key, data, new CacheItemPolicy() { Priority = CacheItemPriority.Default });
        }
                
        public void Add(string key, object data, DateTime absoluteExpiration)
        {
            MemoryCache.Default.Add(key, data, absoluteExpiration);
        }
                
        public void Add(string key, object data, TimeSpan slidingExpiration)
        {
            MemoryCache.Default.Add(key, data, new CacheItemPolicy() { SlidingExpiration = slidingExpiration });
        }
               
        public ICollection<string> Keys
        {
            get 
            {
                return MemoryCache.Default.Select(k => k.Key).ToList();
            }
        }
    }
}

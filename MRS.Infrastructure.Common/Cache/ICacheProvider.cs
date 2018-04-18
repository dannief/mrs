using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace MRS.Infrastructure.Common.Cache
{
    public interface ICacheProvider
    {
        T Get<T>(string key);
        void Remove(string key);
        void Add(string key, object data);
        void Add(string key, object data, DateTime absoluteExpiration);
        void Add(string key, object data, TimeSpan slidingExpiration);        
        ICollection<string> Keys { get; }
    }
}

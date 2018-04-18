using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace MRS.Website.Code
{   
    /// <summary>
    /// JSON Serialization and Deserialization Assistant Class
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string Serialize<T>(this object obj)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));            
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, (T)obj);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T Deserialize<T>(this string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
    }
}
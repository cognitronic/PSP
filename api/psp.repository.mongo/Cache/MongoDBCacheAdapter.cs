using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using psp.core;
using psp.repository.mongo.Providers;

namespace psp.repository.mongo.Cache
{
    public class MongoDBCacheAdapter : ICacheStorage
    {
        private readonly static MongoDBOutputCacheProvider _instance;

        static MongoDBCacheAdapter()
        {
            var map = new System.Collections.Specialized.NameValueCollection();
            map.Add("database", "ASPNETDB");
            map.Add("collection", "OutputCache");
            _instance = new MongoDBOutputCacheProvider("mongodb://localhost", map);
        }

        public static MongoDBOutputCacheProvider Instance { get { return _instance; } }
        public void Remove(string key)
        {
            Instance.Remove(key);
        }

        public void Store(string key, object data)
        {
            Instance.Add(key, data, DateTime.Now.AddDays(7));
        }

        public T Get<T>(string key)
        {
            try
            {
                T itemStored = (T)Instance.Get(key);
                if (itemStored == null)
                    itemStored = default(T);
                return itemStored;
            }
            catch (NullReferenceException exc)
            {
                return default(T);
            }
        }
    }
}

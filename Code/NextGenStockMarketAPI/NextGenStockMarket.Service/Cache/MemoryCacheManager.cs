using System;
using System.Linq;
using System.Runtime.Caching;
namespace Core.Cache
{
    public  class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;

        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }
        public virtual void RemoveByStartwith(string pattern)
        {
            var memoryCache = MemoryCache.Default;
            var keys = memoryCache.Select(p => p.Key).ToList();
            foreach (var item in keys)
            {
                if (item.StartsWith(pattern) && memoryCache.Contains(item))
                {
                    memoryCache.Remove(item);
                }
            }
        }
        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
        }
        public virtual void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
        public virtual void Dispose()
        {
        }
    }
}

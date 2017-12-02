using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ProjectBase.Utility
{
    /// <summary>
    /// Manages cache interactions.
    /// </summary>
    public class CacheManager
    {
        public static CacheItemRemovedCallback CacheRemovedCallBack = null;
        /// <summary>
        /// Adds a value to cache.
        /// </summary>
        public static void AddToCache(string Key, object obj, DateTime AbsoluteExpiration, CacheItemPriority Priority = CacheItemPriority.Default)
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                HttpContext.Current.Cache.Add(Key, obj, null, AbsoluteExpiration, Cache.NoSlidingExpiration, Priority, CacheRemovedCallBack);
            }
            else
                throw new Exception("Cache is not usable");
        }
        /// <summary>
        /// Adds a value to cache.
        /// </summary>
        public static void AddToCache(string Key, object obj, TimeSpan SlidingExpiration, CacheItemPriority Priority = CacheItemPriority.Default)
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                HttpContext.Current.Cache.Add(Key, obj, null, Cache.NoAbsoluteExpiration, SlidingExpiration, Priority, CacheRemovedCallBack);
            }
            else
                throw new Exception("Cache is not usable");
        }
        /// <summary>
        /// Adds a value to cache for 4 sec.
        /// </summary>
        public static void AddToShortTimeCache(string Key, object obj, CacheItemPriority Priority = CacheItemPriority.Default)
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                HttpContext.Current.Cache.Add(Key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 4), Priority, CacheRemovedCallBack);
            }
            else
                throw new Exception("Cache is not usable");
        }
        /// <summary>
        /// Gets a value that is stored in cache with a given key.
        /// </summary>
        public static object GetFromCache(string Key)
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache != null)
            {
                return HttpContext.Current.Cache[Key];
            }
            else
                throw new Exception("Cache is not usable");
        }
    }
}

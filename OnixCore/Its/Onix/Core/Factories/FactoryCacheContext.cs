using System;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

using Its.Onix.Core.Caches;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryCacheContext
    {
        private static readonly string defaultProfile = "DEFAULT";

        private static ILoggerFactory loggerFactory = null;
        private static Hashtable classMaps = new Hashtable();

        private static Dictionary<string, Dictionary<string, ICacheContext>> cacheProfileMaps = new Dictionary<string, Dictionary<string, ICacheContext>>();

        public static void RegisterCache(string name, string fqdn)
        {
            classMaps.Add(name, fqdn);
        }
        
        public static void RegisterCaches(Dictionary<string, string> caches)
        {
            foreach(KeyValuePair<string, string> cache in caches)
            {
                RegisterCache(cache.Key, cache.Value);
            }            
        }        

        static FactoryCacheContext()
        {
        }

        public static ICacheContext GetCacheObject(string name)
        {
            ICacheContext ctx = GetCacheObject(defaultProfile, name);
            return ctx;
        }

        public static ICacheContext GetCacheObject(string profile, string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new ArgumentNullException(String.Format("Cache name not found [{0}]", name));
            }

            Dictionary<string, ICacheContext> cacheMaps = null;
            if (!cacheProfileMaps.ContainsKey(profile))
            {
                cacheMaps = new Dictionary<string, ICacheContext>();
                cacheProfileMaps.Add(profile, cacheMaps);
            }
            else
            {
                cacheMaps = cacheProfileMaps[profile];
            }

            ICacheContext cacheObj = null;
            if (!cacheMaps.ContainsKey(name))
            {
                //Create just only one time and reuse it later
                //Using lazy approach

                string[] tokens = className.Split(':');
                
                string assemblyName = tokens[0];
                className = tokens[1];

                Assembly asm = Assembly.LoadFrom(assemblyName);                    
                
                cacheObj = (ICacheContext)asm.CreateInstance(className);
                cacheMaps.Add(name, cacheObj);

                if (loggerFactory != null)
                {
                    Type t = cacheObj.GetType();
                    ILogger logger = loggerFactory.CreateLogger(t);
                    cacheObj.SetLogger(logger);
                }
            }
            else
            {
                cacheObj = cacheMaps[name];
            }

            return (cacheObj);
        }

        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }
    }

}

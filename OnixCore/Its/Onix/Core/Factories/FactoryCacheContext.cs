using System;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.Caches;
using Its.Onix.Core.Commons.Plugin;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryCacheContext
    {

        private static ILoggerFactory loggerFactory = null;
        private static Dictionary<string, PluginEntry> classMaps = new Dictionary<string, PluginEntry>();

        private static Dictionary<string, Dictionary<string, ICacheContext>> cacheProfileMaps = new Dictionary<string, Dictionary<string, ICacheContext>>();

        public static void RegisterCache(Assembly asm, string name, string fqdn)
        {
            FactoryContextUtils.RegisterItem(classMaps, asm, name, fqdn);
        }
        
        public static void RegisterCaches(Dictionary<string, PluginEntry> caches)
        {
            FactoryContextUtils.RegisterItems(classMaps, caches);
        }        

        static FactoryCacheContext()
        {
        }

        public static void ClearRegisteredItems()
        {
            classMaps.Clear();
        }

        public static ICacheContext GetCacheObject(string name)
        {
            ICacheContext ctx = GetCacheObject(FactoryContextUtils.DefaultProfileName, name);
            return ctx;
        }

        public static ICacheContext GetCacheObject(string profile, string name)
        {
            if (!classMaps.ContainsKey(name))
            {
                throw new ArgumentNullException(String.Format("Cache name not found [{0}]", name));
            }

            PluginEntry entry = classMaps[name];
            
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

                Assembly asm = Assembly.Load(entry.Asm.GetName());                    
                
                cacheObj = (ICacheContext)asm.CreateInstance(entry.Fqdn);
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

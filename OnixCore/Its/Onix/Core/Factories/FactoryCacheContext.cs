using System;
using System.IO;
using System.Collections;
using System.Reflection;

using Its.Onix.Core.Caches;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryCacheContext
    {
        private static ILoggerFactory loggerFactory = null;
        private static Hashtable classMaps = new Hashtable();
        private static Hashtable objectMaps = new Hashtable();

        public static void RegisterCache(string name, string fqdn)
        {
            classMaps.Add(name, fqdn);
        }

        static FactoryCacheContext()
        {
        }

        public static ICacheContext GetCacheObject(string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new ArgumentNullException(String.Format("Cache name not found [{0}]", name));
            }

            ICacheContext cacheObj = (ICacheContext)objectMaps[name];
            if (cacheObj == null)
            {
                //Create just only one time and reuse it later
                //Using lazy approach

                string[] tokens = className.Split(':');
                
                string assemblyName = tokens[0];
                className = tokens[1];

                Assembly asm = Assembly.LoadFrom(assemblyName);                    
                
                cacheObj = (ICacheContext)asm.CreateInstance(className);
                objectMaps[name] = cacheObj;

                if (loggerFactory != null)
                {
                    Type t = cacheObj.GetType();
                    ILogger logger = loggerFactory.CreateLogger(t);
                    cacheObj.SetLogger(logger);
                }
            }

            return (cacheObj);
        }

        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }
    }

}
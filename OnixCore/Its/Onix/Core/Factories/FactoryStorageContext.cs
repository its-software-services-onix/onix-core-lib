using System;
using System.Collections;
using System.Reflection;

using Its.Onix.Core.Storages;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryStorageContext
    {
        private static ILoggerFactory loggerFactory = null;
        private static Hashtable classMaps = new Hashtable();

        private static void addClassConfig(string apiName, string fqdn)
        {
            classMaps.Add(apiName, fqdn);
        }

        static FactoryStorageContext()
        {
            initClassMap();
        }

        private static void initClassMap()
        {
            addClassConfig("FirebaseStorageContext", "Its.Onix.Core.Storages.FirebaseStorageContext");
        }

        public static IStorageContext CreateStorageObject(string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new ArgumentNullException(String.Format("Class not found [{0}]", name));
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            IStorageContext obj = (IStorageContext)asm.CreateInstance(className);            

            if (loggerFactory != null)
            {
                Type t = obj.GetType();
                ILogger logger = loggerFactory.CreateLogger(t);
                obj.SetLogger(logger);
            }

            return(obj);
        }
        
        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }
    }

}
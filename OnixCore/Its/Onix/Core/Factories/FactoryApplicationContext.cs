using System;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.Applications;
using Its.Onix.Core.Commons.Plugin;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryApplicationContext
    {
        private static ILoggerFactory loggerFactory = null;
        private static Dictionary<string, PluginEntry> classMaps = new Dictionary<string, PluginEntry>();

        static FactoryApplicationContext()
        {
        }

        public static void ClearRegisteredItems()
        {
            classMaps.Clear();
        }

        public static void RegisterApplication(Assembly asm, string apiName, string fqdn)
        {
            FactoryContextUtils.RegisterItem(classMaps, asm, apiName, fqdn);
        }

        public static void RegisterApplications(Dictionary<string, PluginEntry> applications)
        {
            FactoryContextUtils.RegisterItems(classMaps, applications);
        }

        public static IApplication CreateApplicationObject(string name)
        {
            if (!classMaps.ContainsKey(name))
            {
                throw new ArgumentNullException(String.Format("Application not found [{0}]", name));
            }


            PluginEntry entry = classMaps[name];         

            Assembly asm = Assembly.Load(entry.Asm.GetName());  
            IApplication obj = (IApplication)asm.CreateInstance(entry.Fqdn);         

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
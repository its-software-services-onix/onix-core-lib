using System;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.Databases;
using Its.Onix.Core.Commons.Plugin;

namespace Its.Onix.Core.Factories
{
    public static class FactoryDbContext
    {
        private static Dictionary<string, PluginEntry> classMaps = new Dictionary<string, PluginEntry>();

        static FactoryDbContext()
        {
        }

        public static void ClearRegisteredItems()
        {
            classMaps.Clear();
        }

        public static void RegisterDbContext(Assembly asm, string name, string fqdn)
        {
            FactoryContextUtils.RegisterItem(classMaps, asm, name, fqdn);
        }

        public static void RegisterDbContexts(Dictionary<string, PluginEntry> applications)
        {
            FactoryContextUtils.RegisterItems(classMaps, applications);
        }

        public static BaseDbContext CreateDbContextObject(string name, DbCredential credential)
        {
            if (!classMaps.ContainsKey(name))
            {
                throw new ArgumentNullException(String.Format("DbContext not found [{0}]", name));
            }

            PluginEntry entry = classMaps[name];

            Assembly asm = Assembly.Load(entry.Asm.GetName());  
            Type t = asm.GetType(entry.Fqdn);
            
            BaseDbContext obj = (BaseDbContext) Activator.CreateInstance(t, credential);
            return(obj);
        }        
    }

}
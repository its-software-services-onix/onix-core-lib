using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.Commons.Plugin;


namespace Its.Onix.Core.Factories
{
    public static class FactoryContextUtils
    {
        public static readonly string DefaultProfileName = "DEFAULT";

        public static void AddItem(Hashtable items, string apiName, string fqdn)
        {
            items.Add(apiName, fqdn);
        }

        public static void RegisterItem(Dictionary<string, PluginEntry> dests, Assembly asm, string name, string fqdn)
        {
            PluginEntry entry = new PluginEntry(asm, name, fqdn);
            dests[name] = entry;
        }

        public static void RegisterItems(Dictionary<string, PluginEntry> dests, Dictionary<string, PluginEntry> items)
        {
            foreach(KeyValuePair<string, PluginEntry> item in items)
            {
                PluginEntry entry = item.Value;
                RegisterItem(dests, entry.Asm, entry.Key, entry.Fqdn);
            }             
        }
    }
}
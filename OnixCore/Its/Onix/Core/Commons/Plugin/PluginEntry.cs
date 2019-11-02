using System.Reflection;

namespace Its.Onix.Core.Commons.Plugin
{
	public class PluginEntry
	{
        public string Key {get; set;}
        public string Fqdn {get; set;}
        public Assembly Asm {get; set;}

        public PluginEntry(Assembly assembly, string key, string fqdn)
        {
            Asm = assembly;
            Key = key;
            Fqdn = fqdn;
        }
    }
}

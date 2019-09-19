using System;

using NDesk.Options;
using Its.Onix.Core.Commons.Table;

namespace Its.Onix.Core.Applications
{
    public class ConsoleAppBaseMocked : ConsoleAppBase
    {
        private string pv = "FirebaseNoSqlContext";

        public ConsoleAppBaseMocked(string provider)
        {
            pv = provider;
        }

        protected override int Execute()
        {
            string host = Environment.GetEnvironmentVariable("ONIX_FIREBASE_DATABASE");
            string key = Environment.GetEnvironmentVariable("ONIX_FIREBASE_KEY");
            string username = Environment.GetEnvironmentVariable("ONIX_FIREBASE_USERNAME");
            string password = Environment.GetEnvironmentVariable("ONIX_FIREBASE_PASSWORD");

            var o = GetNoSqlContext(pv, host, key, username, password);

            return 0;
        }

        protected override OptionSet PopulateCustomOptionSet(OptionSet options)
        {
            //Do nothing
            return options;
        }         

        public CTable DummyXmlToCTable()
        {
            return XmlToCTable();
        }
    }
}

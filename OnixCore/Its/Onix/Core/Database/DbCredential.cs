using System;

namespace Its.Onix.Core.Databases
{
	public class DbCredential
	{
        private string Host = "";
        private int Port = 0;
        private string Db = "";
        private string UserName = "";
        private string Password = "";
        private string Provider = "";

        private void SetDbCredential(string host, int port, string db, string username, string password, string provider)
        {
            Host = host;
            Port = port;
            Db = db;
            UserName = username;
            Password = password;
            Provider = provider;
        }

        public DbCredential(string host, int port, string db, string username, string password)
        {
            SetDbCredential(host, port, db, username, password, "pgsql");
        }

        public DbCredential(string host, int port, string db, string username, string password, string provider)
        {
            SetDbCredential(host, port, db, username, password, provider);
        }        

        public bool IsProviderPgSql()
        {
            return Provider.Equals("pgsql");
        } 

        public string ConnectionStringPgSql()
        {
            string connStr = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4};", Host, Port, Db, UserName, Password);
            return connStr;
        }              
    }
}

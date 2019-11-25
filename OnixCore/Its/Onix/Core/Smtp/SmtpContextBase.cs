using System;
using System.Net.Mail;
using System.Net;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Smtp
{
	public abstract class SmtpContextBase : ISmtpContext 
	{
        private ILogger appLogger;
        
        private string smtpHost = "";
        private int smtpPort = 0;
        private string smtpUser = "";
        private string smtpPassword = "";

        public abstract void Send(Mail mail);

        protected SmtpContextBase()
        {
        }

        public void SetSmtpConfigByEnv(string envHost, string envPort, string envUser, string envPassword)
        {
            smtpHost = Environment.GetEnvironmentVariable(envHost);
            smtpPort = Int32.Parse(Environment.GetEnvironmentVariable(envPort));
            smtpUser = Environment.GetEnvironmentVariable(envUser);
            smtpPassword = Environment.GetEnvironmentVariable(envPassword);

            SetSmtpConfig(smtpHost, smtpPort, smtpUser, smtpPassword);
        }

        public void SetSmtpConfig(string host, int port, string user, string password)
        {
            smtpHost = host;
            smtpPort = port;
            smtpUser = user;
            smtpPassword = password;
        }

        protected string GetSmtpPassword()
        {
            return smtpPassword;
        }

        public void SetLogger(ILogger logger)
        {
            appLogger = logger;
        }

        public ILogger GetLogger()
        {
            return appLogger;
        }           
    }    
}

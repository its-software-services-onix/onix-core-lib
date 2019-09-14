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

        protected SmtpContextBase()
        {
        }

        public void SetSmtpConfigByEnv(string envHost, string envPort, string envUser, string envPassword)
        {
            smtpHost = Environment.GetEnvironmentVariable(envHost);
            smtpPort = Int32.Parse(Environment.GetEnvironmentVariable(envPort));
            smtpUser = Environment.GetEnvironmentVariable(envUser);;
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

        public void Send(Mail mail)
        {                   
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mail.From);
            mailMessage.To.Add(mail.To);
            mailMessage.Body = mail.Body;
            mailMessage.Subject = mail.Subject;
            // Will need to add - client dot Send(mailMessage); here
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

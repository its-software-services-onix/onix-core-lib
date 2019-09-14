using System;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Smtp
{
	public interface ISmtpContext
	{
        void SetSmtpConfig(string host, int port, string user, string password);
        void Send(Mail mail);
        void SetLogger(ILogger logger);
        ILogger GetLogger();        
    }    
}

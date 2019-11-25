using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Notifications
{
	public interface INotification
	{
        void SetNotificationToken(string token);
        void Send(string message);
        void SetLogger(ILogger logger);
        ILogger GetLogger();        
    }    
}

using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Its.Onix.Core.Utils;

namespace Its.Onix.Core.Notifications
{
	public abstract class NotificationBase : INotification 
	{
        protected abstract Process GetExecuteProcess(string message);
        protected abstract bool IsError(string result, string errmsg);

        private ILogger appLogger;        
        private string token = "";

        protected string GetToken()
        {
            return token;
        }

        protected NotificationBase()
        {
        }
    
        public void SetNotificationToken(string token)
        {
            this.token = token;
        }

        public void Send(string message)
        {
            Process proc = GetExecuteProcess(message);
            proc.Start();

            string result = proc.StandardOutput.ReadToEnd();
            string err = proc.StandardError.ReadToEnd();

            proc.WaitForExit();

            if (IsError(result, err))
            {
                LogUtils.LogError(appLogger, "Notification error - [{0}]", err);
            }   
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

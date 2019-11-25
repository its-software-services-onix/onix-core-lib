using System;
using System.Diagnostics;

namespace Its.Onix.Core.Notifications
{
	public class LineNotification : NotificationBase
	{
        public LineNotification() : base()
        {
        }

        protected override Process GetExecuteProcess(string message)
        {
            string endpoint = "https://notify-api.line.me/api/notify";
            string token = GetToken();
            string template = @"curl -s -X POST -H ""Authorization: Bearer {0}"" -F ""message={1}"" {2}";
            
            string command = string.Format(template, token, message, endpoint);
            Process proc = new Process();

            proc.StartInfo.FileName = "curl";
            proc.StartInfo.Arguments = command;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            return proc;
        }

        protected override bool IsError(string result, string errmsg)
        {
            //{"status":200,"message":"ok"}

            return !result.Contains(@"""status"":200");
        }
    }    
}

using System;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Utils
{
	public static class LogUtils
	{
        private static void Log(ILogger logger, LogLevel level, string message, params object[] theObjects)
        {
            if (logger != null)
            {
                logger.Log(level, message, theObjects);
            }
        }

        public static void LogInformation(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Information, message, theObjects);
        }    

        public static void LogError(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Error, message, theObjects);
        } 

        public static void LogCritical(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Critical, message, theObjects);
        }  

        public static void LogWarning(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Warning, message, theObjects);
        }  

        public static void LogDebug(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Debug, message, theObjects);
        }  

        public static void LogTrace(ILogger logger, string message, params object[] theObjects)
        {
            Log(logger, LogLevel.Trace, message, theObjects);
        }                                   
    }    
}
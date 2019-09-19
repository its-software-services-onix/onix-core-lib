using System;
using System.Collections;
using System.Reflection;

using Its.Onix.Core.Smtp;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactorySmtpContext
    {
        private static ILoggerFactory loggerFactory = null;
        private static Hashtable classMaps = new Hashtable();

        static FactorySmtpContext()
        {
            InitClassMap();
        }

        private static void InitClassMap()
        {
            FactoryContextUtils.AddItem(classMaps, "SendGridSmtpContext", "Its.Onix.Core.Smtp.SendGridSmtpContext");
        }

        public static ISmtpContext CreateSmtpObject(string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new ArgumentNullException(String.Format("Class not found [{0}]", name));
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            ISmtpContext obj = (ISmtpContext)asm.CreateInstance(className);            

            if (loggerFactory != null)
            {
                Type t = obj.GetType();
                ILogger logger = loggerFactory.CreateLogger(t);
                obj.SetLogger(logger);
            }

            return(obj);
        }
        
        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }
    }

}
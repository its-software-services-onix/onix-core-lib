using System;
using System.Collections;
using System.Reflection;

using Its.Onix.Core.NoSQL;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    public static class FactoryNoSqlContext
    {
        private static ILoggerFactory loggerFactory = null;
        private static Hashtable classMaps = new Hashtable();

        static FactoryNoSqlContext()
        {
            InitClassMap();
        }

        private static void InitClassMap()
        {
            FactoryContextUtils.AddItem(classMaps, "FirebaseNoSqlContext", "Its.Onix.Core.NoSQL.FirebaseNoSqlContext");
        }

        public static INoSqlContext CreateNoSqlObject(string name)
        {
            string className = (string)classMaps[name];
            if (className == null)
            {
                throw new ArgumentNullException(String.Format("Class not found [{0}]", name));
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            INoSqlContext obj = (INoSqlContext)asm.CreateInstance(className);            

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
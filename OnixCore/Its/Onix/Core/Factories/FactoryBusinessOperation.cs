using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Smtp;
using Its.Onix.Core.Business;
using Its.Onix.Core.Commons.Plugin;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{   
    class ContextGroup
    {
        public INoSqlContext NoSqlContext {get; set;}
        public IStorageContext StorageContext {get; set;}
        public ISmtpContext SmtpContext {get; set;}
    }

    public static class FactoryBusinessOperation
    {
        private static readonly string defaultProfile = "DEFAULT";
        private static ILoggerFactory loggerFactory = null;

        private static Dictionary<string, PluginEntry> classMaps = new Dictionary<string, PluginEntry>();
        private static Hashtable contextProfiles = new Hashtable();

        static FactoryBusinessOperation()
        {
        }

        public static void ClearRegisteredItems()
        {
            classMaps.Clear();
        }
        
        public static void RegisterBusinessOperation(Assembly asm, string apiName, string fqdn)
        {
            PluginEntry entry = new PluginEntry(asm, apiName, fqdn);
            classMaps.Add(apiName, entry);            
        }

        public static void RegisterBusinessOperations(Dictionary<string, PluginEntry> operations)
        {
            foreach(KeyValuePair<string, PluginEntry> operation in operations)
            {
                PluginEntry entry = operation.Value;
                RegisterBusinessOperation(entry.Asm, entry.Key, entry.Fqdn);
            }             
        }

        private static ContextGroup GetContextGroup(string profile)
        {
            ContextGroup grp = null;
            if (contextProfiles.Contains(profile))
            {
                grp = (ContextGroup) contextProfiles[profile];                
            }
            else
            {
                grp = new ContextGroup();
                contextProfiles[profile] = grp; 
            }

            return grp;
        }

        #region NoSQLContext
        public static void SetNoSqlContext(string profile, INoSqlContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.NoSqlContext = ctx;
        }        

        public static INoSqlContext GetNoSqlContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.NoSqlContext;
        }   

        public static void SetNoSqlContext(INoSqlContext ctx)
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            grp.NoSqlContext = ctx;
        }        

        public static INoSqlContext GetNoSqlContext()
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            return grp.NoSqlContext;
        }            
        #endregion NoSQLContext


        #region StorageContext
        public static void SetStorageContext(string profile, IStorageContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.StorageContext = ctx;
        }

        public static IStorageContext GetStorageContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.StorageContext;
        }  

        public static void SetStorageContext(IStorageContext ctx)
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            grp.StorageContext = ctx;
        }

        public static IStorageContext GetStorageContext()
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            return grp.StorageContext;
        }                
        #endregion StorageContext


        #region SmtpContext
        public static void SetSmtpContext(string profile, ISmtpContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.SmtpContext = ctx;
        }

        public static ISmtpContext GetSmtpContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.SmtpContext;
        }          

        public static void SetSmtpContext(ISmtpContext ctx)
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            grp.SmtpContext = ctx;
        }

        public static ISmtpContext GetSmtpContext()
        {
            ContextGroup grp = GetContextGroup(defaultProfile);
            return grp.SmtpContext;
        }           
        #endregion SmtpContext


        public static IBusinessOperation CreateBusinessOperationObject(string profile, string name)
        {        
            if (!classMaps.ContainsKey(name))
            {
                throw new ArgumentNullException(String.Format("Operation not found [{0}]", name));
            }

            PluginEntry entry = classMaps[name];         

            Assembly asm = Assembly.Load(entry.Asm.GetName());  
            IBusinessOperation obj = (IBusinessOperation)asm.CreateInstance(entry.Fqdn);
            
            ContextGroup grp = GetContextGroup(profile);

            obj.SetNoSqlContext(grp.NoSqlContext);
            obj.SetStorageContext(grp.StorageContext);
            obj.SetSmtpContext(grp.SmtpContext);

            if (loggerFactory != null)
            {
                Type t = obj.GetType();
                ILogger logger = loggerFactory.CreateLogger(t);
                obj.SetLogger(logger);
            }

            return(obj);
        }

        public static IBusinessOperation CreateBusinessOperationObject(string name)
        {        
            IBusinessOperation obj = CreateBusinessOperationObject(defaultProfile, name);
            return(obj);
        }

        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }         
    } 
}

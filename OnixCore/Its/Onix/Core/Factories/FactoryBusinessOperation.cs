using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Smtp;
using Its.Onix.Core.Databases;
using Its.Onix.Core.Business;
using Its.Onix.Core.Commons.Plugin;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Factories
{
    internal class ContextGroup
    {
        public INoSqlContext NoSqlContext { get; set; }
        public IStorageContext StorageContext { get; set; }
        public ISmtpContext SmtpContext { get; set; }
        public BaseDbContext DatabaseContext { get; set; }
    }

    public static class FactoryBusinessOperation
    {
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
            FactoryContextUtils.RegisterItem(classMaps, asm, apiName, fqdn);
        }

        public static void RegisterBusinessOperations(Dictionary<string, PluginEntry> operations)
        {
            FactoryContextUtils.RegisterItems(classMaps, operations);
        }

        private static ContextGroup GetContextGroup(string profile)
        {
            ContextGroup grp = null;
            if (contextProfiles.Contains(profile))
            {
                grp = (ContextGroup)contextProfiles[profile];
            }
            else
            {
                grp = new ContextGroup();
                contextProfiles[profile] = grp;
            }

            return grp;
        }

        #region NoSQLContext
        public static INoSqlContext GetNoSqlContext()
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            return grp.NoSqlContext;
        }

        public static INoSqlContext GetNoSqlContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.NoSqlContext;
        }

        public static void SetNoSqlContext(INoSqlContext ctx)
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            grp.NoSqlContext = ctx;
        }

        public static void SetNoSqlContext(string profile, INoSqlContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.NoSqlContext = ctx;
        }
        #endregion NoSQLContext


        #region StorageContext
        public static IStorageContext GetStorageContext()
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            return grp.StorageContext;
        }
        public static IStorageContext GetStorageContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.StorageContext;
        }

        public static void SetStorageContext(IStorageContext ctx)
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            grp.StorageContext = ctx;
        }

        public static void SetStorageContext(string profile, IStorageContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.StorageContext = ctx;
        }
        #endregion StorageContext


        #region SmtpContext
        public static ISmtpContext GetSmtpContext()
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            return grp.SmtpContext;
        }

        public static ISmtpContext GetSmtpContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.SmtpContext;
        }

        public static void SetSmtpContext(ISmtpContext ctx)
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            grp.SmtpContext = ctx;
        }

        public static void SetSmtpContext(string profile, ISmtpContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.SmtpContext = ctx;
        }
        #endregion SmtpContext



        #region DatabaseContext
        public static BaseDbContext GetDatabaseContext()
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            return grp.DatabaseContext;
        }

        public static BaseDbContext GetDatabaseContext(string profile)
        {
            ContextGroup grp = GetContextGroup(profile);
            return grp.DatabaseContext;
        }

        public static void SetDatabaseContext(BaseDbContext ctx)
        {
            ContextGroup grp = GetContextGroup(FactoryContextUtils.DefaultProfileName);
            grp.DatabaseContext = ctx;
        }

        public static void SetDatabaseContext(string profile, BaseDbContext ctx)
        {
            ContextGroup grp = GetContextGroup(profile);
            grp.DatabaseContext = ctx;
        }
        #endregion DatabaseContext

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
            obj.SetDatabaseContext(grp.DatabaseContext);

            if (loggerFactory != null)
            {
                Type t = obj.GetType();
                ILogger logger = loggerFactory.CreateLogger(t);
                obj.SetLogger(logger);
            }

            return (obj);
        }

        public static IBusinessOperation CreateBusinessOperationObject(string name)
        {
            IBusinessOperation obj = CreateBusinessOperationObject(FactoryContextUtils.DefaultProfileName, name);
            return (obj);
        }

        public static void SetLoggerFactory(ILoggerFactory logFact)
        {
            loggerFactory = logFact;
        }
    }
}

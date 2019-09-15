using System;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Smtp;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Business
{    
	public abstract class BusinessOperationBase : IBusinessOperation
	{
        private ILogger appLogger;

        private INoSqlContext noSqlContext = null;
        private IStorageContext storageContext = null;
        private ISmtpContext smtpContext = null;

        public void SetNoSqlContext(INoSqlContext context)
        {
            noSqlContext = context;
        }

        public INoSqlContext GetNoSqlContext()
        {
            return noSqlContext;
        }


        public void SetStorageContext(IStorageContext context)
        {
            storageContext = context;
        }

        public IStorageContext GetStorageContext()
        {
            return storageContext;
        }


        public void SetSmtpContext(ISmtpContext context)
        {
            smtpContext = context;
        }

        public ISmtpContext GetSmtpContext()
        {
            return smtpContext;
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

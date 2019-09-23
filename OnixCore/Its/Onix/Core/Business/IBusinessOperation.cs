using System;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Databases;
using Its.Onix.Core.Smtp;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Business
{
	public interface IBusinessOperation
	{
        void SetNoSqlContext(INoSqlContext context);
        void SetStorageContext(IStorageContext context);
        void SetSmtpContext(ISmtpContext context); 
        void SetDatabaseContext(BaseDbContext context); 

        INoSqlContext GetNoSqlContext();
        IStorageContext GetStorageContext();
        ISmtpContext GetSmtpContext(); 
        BaseDbContext GetDatabaseContext(); 

        void SetAutoCommit(bool autoCommit);
        bool GetAutoCommit();

        void SetLogger(ILogger logger);
        ILogger GetLogger();
    }
}

using System;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using NDesk.Options;

using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Applications
{
	public interface IApplication
	{
        int Run();
        OptionSet CreateOptionSet();
        void SetNoSqlContext(INoSqlContext context);
        INoSqlContext GetNoSqlContext();

        void SetStorageContext(IStorageContext context);
        IStorageContext GetStorageContext();

        void SetLogger(ILogger logger);
        ILogger GetLogger();
    }
}

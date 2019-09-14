using System;

namespace Its.Onix.Core.NoSQL
{
	public interface INoSqlTokenRefreshAble
	{
        void SetRefreshInterval(long refreshRate);
        DateTime GetLastRefreshDtm();
        void SetLastRefreshDtm(DateTime refreshDtm);
    }    
}

using System;

namespace Its.Onix.Core.NoSQL
{
	public interface INoSQLTokenRefreshAble
	{
        void SetRefreshInterval(long refreshRate);
        DateTime GetLastRefreshDtm();
        void SetLastRefreshDtm(DateTime refreshDtm);
    }    
}

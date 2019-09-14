using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

using Its.Onix.Core.Commons.Model;

namespace Its.Onix.Core.Caches
{
	public interface ICacheContext
	{
        void SetRefreshInterval(long tickCount);
        DateTime GetLastRefreshDtm();
        void SetLastRefreshDtm(DateTime dtm);
        
        Dictionary<string, BaseModel> GetValues();
        BaseModel GetValue(string key);

        void SetLogger(ILogger logger);
        ILogger GetLogger();        
    }    
}

using System;
using System.Collections.Generic;
using Its.Onix.Core.Commons.Model;

namespace Its.Onix.Core.Caches
{
    public class CacheMockedForTesting : CacheBase
    {
        public CacheMockedForTesting()
        {
        }

        protected override Dictionary<string, BaseModel> LoadContents()
        {
            var map = new Dictionary<string, BaseModel>();

            map.Add("KEY3", new BaseModel() { Key = "ID3"});
            map.Add("KEY4", new BaseModel() { Key = "ID4"});

            return map;
        }
    }
}
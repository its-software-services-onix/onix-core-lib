using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using Moq;
using NUnit.Framework;
using Its.Onix.Core.Commons.Model;

namespace Its.Onix.Core.Caches
{
    public class CacheBaseTest
    {
        private CacheMockedForTesting cache = new CacheMockedForTesting();
        private Dictionary<string, BaseModel> contents = new Dictionary<string, BaseModel>();

        [SetUp]
        public void Setup()
        {
            contents.Clear();
            contents.Add("KEY1", new BaseModel() { Key = "ID1"});
            contents.Add("KEY2", new BaseModel() { Key = "ID2"});
        }

        [TestCase]
        public void AssignLoggerTest()
        {
            var logger = new Mock<ILogger>().Object;
            cache.SetLogger(logger);
            var outLogger = cache.GetLogger();
            Assert.True(logger == outLogger);
        }

        [TestCase]
        public void GetLastRefreshDtmTest()
        {
            DateTime currentDtm = DateTime.Now;
            cache.SetLastRefreshDtm(currentDtm);
            DateTime outDtm = cache.GetLastRefreshDtm();
            Assert.True(currentDtm == outDtm);
        }

        [TestCase("KEY1", "ID1")]
        [TestCase("KEY2", "ID2")]
        public void SetAndGetContentsTest(string key, string value)
        {
            cache.SetContents(contents);
            cache.SetLastRefreshDtm(DateTime.Now); //Just refreshed so no need to refresh this time

            var obj = cache.GetValue(key);

            Assert.AreEqual(value, obj.Key, "Value from cache not match!!!");
        }

        [TestCase("KEY3", "ID3")]
        [TestCase("KEY4", "ID4")]
        public void CacheReloadContentsWhenNullTest(string key, string value)
        {
            cache.SetContents(null);
            var obj = cache.GetValue(key);

            Assert.AreEqual(value, obj.Key, "Value from cache not match!!!");
        } 

        [TestCase("KEY3", "ID3")]
        [TestCase("KEY4", "ID4")]
        public void CacheReloadContentsWhenExpireTest(string key, string value)
        {
            cache.SetContents(contents);
            cache.SetLastRefreshDtm(DateTime.Now.AddDays(-1)); //Last refresh time was yesterday
            cache.SetRefreshInterval(TimeSpan.TicksPerMinute); //1 minute
            
            var obj = cache.GetValue(key);

            Assert.AreEqual(value, obj.Key, "Value from cache not match!!!");
        }                
    }
}
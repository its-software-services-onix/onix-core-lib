using System;
using NUnit.Framework;

using Moq;
using Microsoft.Extensions.Logging;
using Its.Onix.Core.Caches;

namespace Its.Onix.Core.Factories
{
    public class FactoryCacheContextTest
    {
        [SetUp]
        public void Setup()
        {            
        }

        [TestCase("UnknowName")]
        public void UnknownApiNameTest(string name)
        {
            try
            {
                ICacheContext opt = FactoryCacheContext.GetCacheObject(name);
                Assert.True(false, "Exception shoud be throw for unknow API!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("CacheMockedForTesting")]
        public void KnownNameTest(string name)
        {
            FactoryCacheContext.RegisterCache("CacheMockedForTesting", "OnixCoreTest.dll:Its.Onix.Core.Caches.CacheMockedForTesting");
            FactoryCacheContext.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            ICacheContext opt = FactoryCacheContext.GetCacheObject(name);            
            Assert.IsNotNull(opt, "Object must not be null!!!");
            
            opt.GetLogger();
        }          
    }    
}
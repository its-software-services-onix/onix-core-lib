using System;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;

using Moq;
using Microsoft.Extensions.Logging;
using Its.Onix.Core.Caches;
using Its.Onix.Core.Commons.Plugin;

namespace Its.Onix.Core.Factories
{
    public class FactoryCacheContextTest
    {
        public FactoryCacheContextTest()
        {
            var asm = Assembly.GetExecutingAssembly();

            FactoryCacheContext.ClearRegisteredItems();

            FactoryCacheContext.RegisterCache(asm, "CacheMockedForTesting", "Its.Onix.Core.Caches.CacheMockedForTesting");
            FactoryCacheContext.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            var items = new Dictionary<string, PluginEntry>();
            items.Add("Testing0", new PluginEntry(asm, "Testing0", "Its.Onix.Core.Caches.CacheMockedForTesting"));
            items.Add("Testing1", new PluginEntry(asm, "Testing1", "Its.Onix.Core.Caches.CacheMockedForTesting"));
            items.Add("Testing2", new PluginEntry(asm, "Testing2", "Its.Onix.Core.Caches.CacheMockedForTesting"));
            FactoryCacheContext.RegisterCaches(items);
        }

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
            ICacheContext opt = FactoryCacheContext.GetCacheObject(name);            
            Assert.IsNotNull(opt, "Object must not be null!!!");
            
            opt.GetLogger();
        }  

        [TestCase("Testing0")]
        [TestCase("Testing1")]
        [TestCase("Testing2")]
        public void MultipleItemsRegisterTest(string name)
        {
            ICacheContext opt1 = FactoryCacheContext.GetCacheObject(name);            
            Assert.IsNotNull(opt1, "Object must not be null!!!");
            
            ICacheContext opt2 = FactoryCacheContext.GetCacheObject(name);            
            Assert.AreSame(opt1, opt2, "Should get the same object!!!");

            opt1.GetLogger();
        }                 
    }    
}
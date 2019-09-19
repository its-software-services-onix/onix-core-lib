using System;
using System.Reflection;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

using Its.Onix.Core.Applications;
using Its.Onix.Core.Commons.Plugin;

namespace Its.Onix.Core.Factories
{
    public class FactoryApplicationContextTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("UnknownAppName")]
        public void UnknownApplicationNameTest(string appName)
        {
            try
            {
                IApplication opt = FactoryApplicationContext.CreateApplicationObject(appName);
                Assert.True(false, "Exception shoud be throw for unknow application name!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("ConsoleAppBaseMocked", "Its.Onix.Core.Applications.ConsoleAppBaseMocked")]
        public void KnownApplicationNameTest(string appName, string fqdn)
        {        
            Assembly asm = Assembly.GetExecutingAssembly();

            var logger = new Mock<ILoggerFactory>().Object;
            FactoryApplicationContext.SetLoggerFactory(logger);

            FactoryApplicationContext.ClearRegisteredItems();            
            FactoryApplicationContext.RegisterApplication(asm, appName, fqdn);

            var items = new Dictionary<string, PluginEntry>();
            for (int i=1; i<=5; i++)
            {
                string key = string.Format("{0}_{1}", appName, i);
                var entry = new PluginEntry(asm, key, fqdn);

                items.Add(key, entry);
            }

            FactoryApplicationContext.RegisterApplications(items);

            IApplication opt = FactoryApplicationContext.CreateApplicationObject(appName);
            Assert.IsNotNull(opt, "Object must not be null!!!");
        }          
    }    
}
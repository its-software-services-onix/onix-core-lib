using System;
using System.Reflection;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

using Moq;
using Its.Onix.Core.Business;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Databases;
using Its.Onix.Core.Smtp;
using Its.Onix.Core.Commons.Plugin;

namespace Its.Onix.Core.Factories
{
    public class FactoryBusinessOperationTest
    {
        public FactoryBusinessOperationTest()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            FactoryBusinessOperation.ClearRegisteredItems();

            FactoryBusinessOperation.RegisterBusinessOperation(asm, "BusinessOperationMocked", "Its.Onix.Core.Business.BusinessOperationMocked");
            FactoryBusinessOperation.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            Dictionary<string, PluginEntry> items = new Dictionary<string, PluginEntry>();
            items.Add("BusinessOperationMocked_1", new PluginEntry(asm, "BusinessOperationMocked_1", "Its.Onix.Core.Business.BusinessOperationMocked"));
            items.Add("BusinessOperationMocked_2", new PluginEntry(asm, "BusinessOperationMocked_2", "Its.Onix.Core.Business.BusinessOperationMocked"));
            items.Add("BusinessOperationMocked_3", new PluginEntry(asm, "BusinessOperationMocked_3", "Its.Onix.Core.Business.BusinessOperationMocked"));
            items.Add("BusinessOperationMocked_4", new PluginEntry(asm, "BusinessOperationMocked_4", "Its.Onix.Core.Business.BusinessOperationMocked"));
            FactoryBusinessOperation.RegisterBusinessOperations(items);
        }

        [SetUp]
        public void Setup()
        {                        
        }

        [TestCase("UnknowName")]
        public void UnknownOperationNameWithOutProfileTest(string name)
        {
            try
            {
                IBusinessOperation opt = FactoryBusinessOperation.CreateBusinessOperationObject(name);
                Assert.True(false, "Exception shoud be throw for unknow API!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("UnknowName")]
        public void UnknownOperationNameWithProfileTest(string name)
        {
            try
            {
                IBusinessOperation opt = FactoryBusinessOperation.CreateBusinessOperationObject("DEFAULT", name);
                Assert.True(false, "Exception shoud be throw for unknow API!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        }  

        [TestCase("BusinessOperationMocked")]
        [TestCase("BusinessOperationMocked_1")]
        [TestCase("BusinessOperationMocked_2")]
        [TestCase("BusinessOperationMocked_3")]
        public void RegisterBusinessOperationNoProfileTest(string name)
        {
            IBusinessOperation opt = FactoryBusinessOperation.CreateBusinessOperationObject(name);
            Assert.IsInstanceOf(typeof(BusinessOperationMocked), opt, "Should get the right object instance!!!");
        }

        [TestCase("PROFILE1", "BusinessOperationMocked")]
        public void RegisterBusinessOperationWithProfileTest(string profile, string name)
        {
            IBusinessOperation opt = FactoryBusinessOperation.CreateBusinessOperationObject(profile, name);
            Assert.IsInstanceOf(typeof(BusinessOperationMocked), opt, "Should get the right object instance!!!");
        }

       [TestCase]
        public void ContextWithNoProfileContextGetterSetterTest()
        {
            IStorageContext storageCtx1 = new Mock<IStorageContext>().Object;
            FactoryBusinessOperation.SetStorageContext(storageCtx1);
            IStorageContext storageCtx2 = FactoryBusinessOperation.GetStorageContext();
            Assert.AreSame(storageCtx1, storageCtx2, "Setter and Getter should return the same thing!!!");

            INoSqlContext noSqlCtx1 = new Mock<INoSqlContext>().Object;
            FactoryBusinessOperation.SetNoSqlContext(noSqlCtx1);
            INoSqlContext noSqlCtx2 = FactoryBusinessOperation.GetNoSqlContext();
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");  

            ISmtpContext noSmtpCtx1 = new Mock<ISmtpContext>().Object;
            FactoryBusinessOperation.SetSmtpContext(noSmtpCtx1);
            ISmtpContext noSmtpCtx2 = FactoryBusinessOperation.GetSmtpContext();
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");   

            BaseDbContext databaseCtx1 = new Mock<BaseDbContext>(null).Object;
            FactoryBusinessOperation.SetDatabaseContext(databaseCtx1);
            BaseDbContext databaseCtx2 = FactoryBusinessOperation.GetDatabaseContext();
            Assert.AreSame(databaseCtx1, databaseCtx2, "Setter and Getter should return the same thing!!!");                                  
        } 

       [TestCase("PROFILE1")]
        public void ContextWithProfileGetterSetterTest(string profile)
        {
            IStorageContext storageCtx1 = new Mock<IStorageContext>().Object;
            FactoryBusinessOperation.SetStorageContext(profile, storageCtx1);
            IStorageContext storageCtx2 = FactoryBusinessOperation.GetStorageContext(profile);
            Assert.AreSame(storageCtx1, storageCtx2, "Setter and Getter should return the same thing!!!");

            INoSqlContext noSqlCtx1 = new Mock<INoSqlContext>().Object;
            FactoryBusinessOperation.SetNoSqlContext(profile, noSqlCtx1);
            INoSqlContext noSqlCtx2 = FactoryBusinessOperation.GetNoSqlContext(profile);
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");  

            ISmtpContext noSmtpCtx1 = new Mock<ISmtpContext>().Object;
            FactoryBusinessOperation.SetSmtpContext(profile, noSmtpCtx1);
            ISmtpContext noSmtpCtx2 = FactoryBusinessOperation.GetSmtpContext(profile);
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");

            BaseDbContext databaseCtx1 = new Mock<BaseDbContext>(null).Object;
            FactoryBusinessOperation.SetDatabaseContext(profile, databaseCtx1);
            BaseDbContext databaseCtx2 = FactoryBusinessOperation.GetDatabaseContext(profile);
            Assert.AreSame(databaseCtx1, databaseCtx2, "Setter and Getter should return the same thing!!!");                                 
        }                                           
    }        
}
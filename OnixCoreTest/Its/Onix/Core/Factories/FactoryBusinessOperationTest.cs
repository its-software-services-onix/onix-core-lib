using System;
using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

using Moq;
using Its.Onix.Core.Business;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Smtp;

namespace Its.Onix.Core.Factories
{
    public class FactoryBusinessOperationTest
    {
        public FactoryBusinessOperationTest()
        {
            FactoryBusinessOperation.RegisterBusinessOperation("BusinessOperationMocked", "OnixCoreTest.dll:Its.Onix.Core.Business.BusinessOperationMocked");
            FactoryBusinessOperation.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            Dictionary<string, string> items = new Dictionary<string, string>();
            items.Add("BusinessOperationMocked_1", "OnixCoreTest.dll:Its.Onix.Core.Business.BusinessOperationMocked");
            items.Add("BusinessOperationMocked_2", "OnixCoreTest.dll:Its.Onix.Core.Business.BusinessOperationMocked");
            items.Add("BusinessOperationMocked_3", "OnixCoreTest.dll:Its.Onix.Core.Business.BusinessOperationMocked");
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
        }                                           
    }        
}
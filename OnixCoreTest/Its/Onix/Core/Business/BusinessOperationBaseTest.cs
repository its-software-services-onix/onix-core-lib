using System;

using Microsoft.Extensions.Logging;

using Moq;
using NUnit.Framework;
using Its.Onix.Core.NoSQL;
using Its.Onix.Core.Storages;
using Its.Onix.Core.Smtp;

namespace Its.Onix.Core.Business
{
    public class BusinessOperationBaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void GetterSetterTest()
        {
            BusinessOperationMocked opr = new BusinessOperationMocked();

            ILogger logger1 = new Mock<ILogger>().Object;
            opr.SetLogger(logger1);
            ILogger logger2 = opr.GetLogger();
            Assert.AreSame(logger1, logger2, "Setter and Getter should return the same thing!!!");

            IStorageContext storageCtx1 = new Mock<IStorageContext>().Object;
            opr.SetStorageContext(storageCtx1);
            IStorageContext storageCtx2 = opr.GetStorageContext();
            Assert.AreSame(storageCtx1, storageCtx2, "Setter and Getter should return the same thing!!!");

            INoSqlContext noSqlCtx1 = new Mock<INoSqlContext>().Object;
            opr.SetNoSqlContext(noSqlCtx1);
            INoSqlContext noSqlCtx2 = opr.GetNoSqlContext();
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");  

            ISmtpContext noSmtpCtx1 = new Mock<ISmtpContext>().Object;
            opr.SetSmtpContext(noSmtpCtx1);
            ISmtpContext noSmtpCtx2 = opr.GetSmtpContext();
            Assert.AreSame(noSqlCtx1, noSqlCtx2, "Setter and Getter should return the same thing!!!");                       
        }            
    }
}
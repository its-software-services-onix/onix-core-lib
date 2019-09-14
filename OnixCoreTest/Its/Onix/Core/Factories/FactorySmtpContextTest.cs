using System;
using NUnit.Framework;

using Moq;
using Microsoft.Extensions.Logging;
using Its.Onix.Core.Smtp;

namespace Its.Onix.Core.Factories
{
    public class FactorySmtpContextTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("UnknowName")]
        public void UnknownApiNameTest(string apiName)
        {
            try
            {
                ISmtpContext opt = FactorySmtpContext.CreateSmtpObject(apiName);
                Assert.True(false, "Exception shoud be throw for unknow API!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("SendGridSmtpContext")]
        public void KnownNameTest(string apiName)
        {
            FactorySmtpContext.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            ISmtpContext opt = FactorySmtpContext.CreateSmtpObject(apiName);
            opt.GetLogger();
            Assert.IsNotNull(opt, "Object must not be null!!!");
        }          
    }    
}
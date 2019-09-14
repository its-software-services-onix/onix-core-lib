using System;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Moq;

namespace Its.Onix.Core.Utils
{
    public class LogUtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void LoggerIsNullInfoTest()
        {
            LogUtils.LogInformation(null, "This is error");
            Assert.True(true);
        }   

        [TestCase]
        public void LoggerIsNotNullInfoTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogInformation(logger, "This is error");
            Assert.True(true);
        }             
    }    
}
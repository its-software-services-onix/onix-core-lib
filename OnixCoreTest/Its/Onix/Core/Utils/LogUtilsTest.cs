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

        [TestCase]
        public void LoggerIsNotNullErrorTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogError(logger, "This is error");
            Assert.True(true);
        } 

        [TestCase]
        public void LoggerIsNotNullCriticalTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogCritical(logger, "This is error");
            Assert.True(true);
        }  

        [TestCase]
        public void LoggerIsNotNullTraceTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogTrace(logger, "This is error");
            Assert.True(true);
        }    

        [TestCase]
        public void LoggerIsNotNullDebugTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogDebug(logger, "This is error");
            Assert.True(true);
        }     

        [TestCase]
        public void LoggerIsNotNullWarningTest()
        {
            ILogger logger = new Mock<ILogger>().Object;
            LogUtils.LogWarning(logger, "This is error");
            Assert.True(true);
        }                                           
    }    
}
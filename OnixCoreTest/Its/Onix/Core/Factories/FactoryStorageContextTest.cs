using System;
using NUnit.Framework;

using Moq;
using Microsoft.Extensions.Logging;
using Its.Onix.Core.Storages;

namespace Its.Onix.Core.Factories
{
    public class FactoryStorageContextTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("UnknowName")]
        public void UnknownApiNameTest(string storageName)
        {
            try
            {
                IStorageContext opt = FactoryStorageContext.CreateStorageObject(storageName);
                Assert.True(false, "Exception shoud be throw for unknow name!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("FirebaseStorageContext")]
        public void KnownNameTest(string name)
        {
            FactoryStorageContext.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            IStorageContext opt = FactoryStorageContext.CreateStorageObject(name);
            Assert.IsNotNull(opt, "Object must not be null!!!");
        }          
    }    
}
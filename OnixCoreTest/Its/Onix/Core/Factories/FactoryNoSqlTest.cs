using System;
using NUnit.Framework;

using Moq;
using Microsoft.Extensions.Logging;
using Its.Onix.Core.NoSQL;

namespace Its.Onix.Core.Factories
{
    public class FactoryNoSqlTest
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
                INoSqlContext opt = FactoryNoSqlContext.CreateNoSqlObject(name);
                Assert.True(false, "Exception shoud be throw for unknow name!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        } 

        [TestCase("FirebaseNoSqlContext")]
        public void KnownNameTest(string name)
        {
            FactoryNoSqlContext.SetLoggerFactory(new Mock<ILoggerFactory>().Object);

            INoSqlContext opt = FactoryNoSqlContext.CreateNoSqlObject(name);
            Assert.IsNotNull(opt, "Object must not be null!!!");
        }          
    }    
}
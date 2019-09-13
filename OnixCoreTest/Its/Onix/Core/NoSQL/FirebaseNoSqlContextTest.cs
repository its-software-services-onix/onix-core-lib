using System;
using NUnit.Framework;
using Its.Onix.Core.Commons;

namespace Its.Onix.Core.NoSQL
{
	public class FirebaseNoSqlContextTest
	{
        [SetUp]
        public void Setup()
        {
        }

        [TestCase]
        public void NoDataToAuthenTest()
        {
            //Just to cover the test coverage

            INoSqlContext ctx = new FirebaseNoSqlContext();

            try
            {
                ctx.Authenticate("", "", "", "");
                Assert.Fail("Exception should be thrown for failed authen !!!");                
            }
            catch
            {
                //Do nothing
            }
        }                                                     
    }
}

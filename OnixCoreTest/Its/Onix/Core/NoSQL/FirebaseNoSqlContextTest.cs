using System;
using NUnit.Framework;
using Its.Onix.Core.Commons.Model;

namespace Its.Onix.Core.NoSQL
{
	public class FirebaseNoSqlContextTest
	{
        private readonly FirebaseNoSqlContext ctx = null;

        public FirebaseNoSqlContextTest()
        {
            ctx = new FirebaseNoSqlContext();
            Authen(ctx);
        }

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

        private void Authen(INoSqlContext ctx)
        {
            string host = Environment.GetEnvironmentVariable("ONIX_FIREBASE_DATABASE");
            string key = Environment.GetEnvironmentVariable("ONIX_FIREBASE_KEY");
            string username = Environment.GetEnvironmentVariable("ONIX_FIREBASE_USERNAME");
            string password = Environment.GetEnvironmentVariable("ONIX_FIREBASE_PASSWORD");

            //This is for unit testing only, DO NOT put any production data in this DB
            ctx.Authenticate(host,
                key,
                username,
                password);
        }

        [TestCase]
        public void SuccessAuthenTest()
        {
            //Just to cover the test coverage
            try
            {
                ctx.PostData("unit_testing", DateTime.Now);      
            }
            catch
            {
                Assert.Fail("Please see env variable ONIX_FIREBASE_KEY, ONIX_FIREBASE_USERNAME, ONIX_FIREBASE_PASSWORD !!!");
            }
        }

        [TestCase]
        public void NoDataToPostTest()
        {
            //Just to cover the test coverage

            INoSqlContext ctx = new FirebaseNoSqlContext();

            try
            {
                ctx.PostData("", new String("HELLO"));
                Assert.Fail("Exception should be thrown for no authen ealier !!!");
            }
            catch
            {
                //Do nothing
            }
        } 

        [TestCase]
        public void NoDataToPutTest()
        {
            //Just to cover the test coverage

            try
            {
                ctx.PutData("unit_testing", "unit_testing", new String("HELLO"));
            }
            catch (Exception e)
            {
                //Do nothing
                Assert.Fail(e.Message);                
            }
        }  

        [TestCase]
        public void NoDataToDeleteTest()
        {
            //Just to cover the test coverage

            try
            {
                BaseModel prd = new BaseModel();
                prd.Key = "faked_key";
                ctx.DeleteData("unit_testing", prd);
            }
            catch (Exception e)
            {
                //Do nothing
                Assert.Fail(e.Message);                
            }
        }          

        [TestCase]
        public void GetObjectByKeyTest()
        {
            //Just to cover the test coverage

            try
            {
                ctx.PostData("unit_testing", new BaseModel());
                ctx.GetObjectByKey<BaseModel>("unit_testing");
            }
            catch (Exception e)
            {
                //Do nothing
                Assert.Fail(e.Message);                
            }
        }     

        [TestCase("faked_nodes_not_found")]
        [TestCase("products")]
        public void GetObjectList(string node)
        {
            //Just to cover the test coverage

            try
            {
                ctx.GetObjectList<BaseModel>(node);
            }
            catch (Exception e)
            {
                //Do nothing
                Assert.Fail(e.Message);                
            }
        }  

        [TestCase]
        public void GetSingleObjectTest()
        {
            //Just to cover the test coverage

            try
            {
                ctx.GetSingleObject<BaseModel>("faked_nodes_not_found", "faked_key_not_found");
            }
            catch (Exception e)
            {
                //Do nothing
                Assert.Fail(e.Message);                
            }
        } 

        [TestCase]
        public void RefreshAuthenTokenTest()
        {
            try
            {
                DateTime lastRefreshDtm = ctx.GetLastRefreshDtm();   

                ctx.PostData("unit_testing", DateTime.Now);
                DateTime newRefreshDtm = ctx.GetLastRefreshDtm();

                Assert.AreEqual(lastRefreshDtm, newRefreshDtm, "Last refresh date time should not be changed for this case !!!!");

                ctx.SetRefreshInterval(0);
                ctx.PostData("unit_testing", DateTime.Now);
                newRefreshDtm = ctx.GetLastRefreshDtm();

                Assert.AreNotEqual(lastRefreshDtm, newRefreshDtm, "Token should be refreshed !!!!");
            }
            catch
            {
                Assert.Fail("Please see env variable MAGNUM_FIREBASE_KEY, MAGNUM_DB_USERNAME, MAGNUM_DB_PASSWORD !!!");
            }
        }


        [TestCase(1000, TimeSpan.TicksPerDay, false)]
        [TestCase(TimeSpan.TicksPerDay - TimeSpan.TicksPerMinute, TimeSpan.TicksPerDay, false)]
        [TestCase(TimeSpan.TicksPerDay + TimeSpan.TicksPerMinute, TimeSpan.TicksPerDay, true)]
        public void RefreshAuthenTokenIntervalTest(long tickEarlier, long refreshInterval, bool shouldRefresh)
        {
            //Must not die if pass null
            ctx.SetLogger(null);
            ctx.GetLogger();

            DateTime lastRefreshDtm = DateTime.Now.AddTicks(-1 * tickEarlier);
            ctx.SetLastRefreshDtm(lastRefreshDtm);

            ctx.SetRefreshInterval(refreshInterval);

            ctx.PostData("unit_testing", DateTime.Now);
            DateTime newRefreshDtm = ctx.GetLastRefreshDtm();

            bool isRefreshed = lastRefreshDtm.CompareTo(newRefreshDtm) != 0;

            Assert.AreEqual(shouldRefresh, isRefreshed, "Refresh logic is not in the expected manner!!!");
        }                                                       
    }
}

using System;
using System.IO;
using NUnit.Framework;

namespace Its.Onix.Core.Applications
{
    public class ConsoleAppBaseTest
    {
        [SetUp]
        public void Setup()
        {                     
        }

        [TestCase("FirebaseNoSqlContext", 0)]
        [TestCase("dummy", 1)]
        public void GetNoSqlContextTest(string provider, int retCode)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            mocked.CreateOptionSet();
            
            try
            {
                int cd = mocked.Run();                
                Assert.AreEqual(retCode, cd);              
            }
            catch (Exception)
            {
                Assert.AreEqual(1, retCode, "Expected to get exception once provider is unknown !!!");              
            }            
        }  

        private void AddAuthenArguments(ConsoleAppBaseMocked mocked)
        {
            mocked.SetNoSqlContext(null);

            mocked.AddArgument("bucket", Environment.GetEnvironmentVariable("ONIX_FIREBASE_BUCKET"));
            mocked.AddArgument("host", Environment.GetEnvironmentVariable("ONIX_FIREBASE_DATABASE"));
            mocked.AddArgument("key", Environment.GetEnvironmentVariable("ONIX_FIREBASE_KEY"));
            mocked.AddArgument("user", Environment.GetEnvironmentVariable("ONIX_FIREBASE_USERNAME"));
            mocked.AddArgument("password", Environment.GetEnvironmentVariable("ONIX_FIREBASE_PASSWORD"));
        }

        [TestCase("FirebaseStorageContext")]
        public void GetKnownStorageContextWithAuthenTest(string provider)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            AddAuthenArguments(mocked);

            var ctx = mocked.GetStorageContextWithAuthen(provider);
            Assert.AreNotEqual(null, ctx, "Returned context must not be null!!!");
        }

        [TestCase("unknown_provider")]
        public void GetUnknownStorageContextWithAuthenTest(string provider)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            AddAuthenArguments(mocked);

            try
            {
                var ctx = mocked.GetStorageContextWithAuthen(provider);
                Assert.Fail("Exception should be thrown here!!!");
            }
            catch
            {                
            }
        }        

        [TestCase("FirebaseNoSqlContext")]
        public void GetKnownNoSqlContextWithAuthenTest(string provider)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            AddAuthenArguments(mocked);

            var ctx1 = mocked.GetNoSqlContextWithAuthen(provider);
            var ctx2 = mocked.GetNoSqlContext();

            Assert.AreNotEqual(null, ctx1, "Returned context must not be null!!!");
            Assert.AreEqual(null, ctx2, "Returned context must be null!!!");
        }

        [TestCase("unknown_provider")]
        public void GetUnknownNoSqlContextWithAuthenTest(string provider)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            AddAuthenArguments(mocked);

            try
            {
                var ctx1 = mocked.GetNoSqlContextWithAuthen(provider);
                Assert.Fail("Exception should be thrown here!!!");
            }
            catch
            {
            }

            var ctx2 = mocked.GetNoSqlContext();
            var ctx3 = mocked.GetStorageContext();

            Assert.AreEqual(null, ctx2, "Returned context must be null!!!");
            Assert.AreEqual(null, ctx3, "Returned context must be null!!!");
        }

        [TestCase("FirebaseNoSqlContext")]
        public void SetterAndGetterTest(string provider)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked(provider);
            AddAuthenArguments(mocked);

            mocked.SetNoSqlContext(null);
            mocked.SetStorageContext(null);
            mocked.SetLogger(null);

            mocked.DumpParameter();
            var args = mocked.GetArguments();

            Assert.AreEqual(5, args.Count, "Argument count should match!!!");

            Assert.AreEqual(null, mocked.GetNoSqlContext(), "Returned context must be null!!!");
            Assert.AreEqual(null, mocked.GetStorageContext(), "Returned context must be null!!!");
            Assert.AreEqual(null, mocked.GetLogger(), "Returned context must be null!!!");
        }   

        [TestCase("dummy1.xml")]
        public void XmlToCTableTest(string fname)
        {
            ConsoleAppBaseMocked mocked = new ConsoleAppBaseMocked("");

            string tmpPath = Path.GetTempPath();
            string fullPath = Path.Combine(new string[] {tmpPath, fname});

            string xml = @"<?xml version='1.0' encoding='UTF-8'?>
<API>
    <OBJECT name='PARAM'>
        <FIELD name='FUNCTION_NAME'>mocked</FIELD>
    </OBJECT>
    <OBJECT name=''>
        <FIELD name='FIELD1'>mocked</FIELD>
    </OBJECT>
</API>";

            File.WriteAllText(fullPath, xml);

            mocked.AddArgument("infile", fname);
            mocked.AddArgument("basedir", tmpPath);
            mocked.DummyXmlToCTable();

            Assert.AreEqual(null, mocked.GetLogger(), "Returned context must be null!!!");
        }                                          
    }    
}
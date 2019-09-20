using System;
using System.Reflection;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;

using Its.Onix.Core.Databases;
using Its.Onix.Core.Commons.Plugin;

namespace Its.Onix.Core.Factories
{
    public class FactoryDbContextTest
    {
        public FactoryDbContextTest()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            FactoryDbContext.ClearRegisteredItems();
            FactoryDbContext.RegisterDbContext(asm, "MockedDbContext", "Its.Onix.Core.Databases.MockedDbContext");

            var items = new Dictionary<string, PluginEntry>();
            items.Add("MockedDbContext_1", new PluginEntry(asm, "MockedDbContext_1", "Its.Onix.Core.Databases.MockedDbContext"));
            
            FactoryDbContext.RegisterDbContexts(items);
        }

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("UnknownAppName", "host", 999, "db", "user", "password", "pgsql")]
        [TestCase("UnknownAppName", "host", 999, "db", "user", "password", "")]
        public void UnknownDbContextNameTest(string ctxName, string host, int port, string db, string uname, string pw, string provider)
        {
            try
            {
                DbCredential credential = null; 
                if (string.IsNullOrEmpty(provider))
                {
                    credential = new DbCredential(host, port, db, uname, pw);
                }
                else
                {
                    credential = new DbCredential(host, port, db, uname, pw, provider);
                }

                BaseDbContext opt = FactoryDbContext.CreateDbContextObject(ctxName, credential);
                Assert.True(false, "Exception shoud be throw for unknow application name!!!");
            }
            catch (Exception e)
            {
                Assert.True(true, e.Message);
            }
        }   

        [TestCase("MockedDbContext", "host", 999, "db", "user", "password", "pgsql")]
        [TestCase("MockedDbContext_1", "host", 999, "db", "user", "password", "")]
        public void knownDbContextNameTest(string ctxName, string host, int port, string db, string uname, string pw, string provider)
        {
            try
            {
                DbCredential credential = null; 
                if (string.IsNullOrEmpty(provider))
                {
                    credential = new DbCredential(host, port, db, uname, pw);
                }
                else
                {
                    credential = new DbCredential(host, port, db, uname, pw, provider);
                }

                BaseDbContext opt = FactoryDbContext.CreateDbContextObject(ctxName, credential);

                Assert.IsNotNull(opt, "Object created from factory must be null!!!");
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }             
    }    
}
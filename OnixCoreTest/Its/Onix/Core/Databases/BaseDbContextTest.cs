using System;

using Microsoft.EntityFrameworkCore;

using Moq;
using NUnit.Framework;

namespace Its.Onix.Core.Databases
{
    public class BaseDbContextTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("host", 999, "db", "user", "password", "")]
        [TestCase("host", 999, "db", "user", "password", "pgsql")]
        public void ConfigureObjectTest(string host, int port, string db, string uname, string pw, string provider)
        {
            var credential = new DbCredential(host, port, db, uname, pw, provider);
            BaseDbContext ctx = new BaseDbContext(credential);
            ctx.TestOnConfiguring(new DbContextOptionsBuilder());   

            var cr = ctx.GetCredential();
            Assert.AreSame(credential, cr, "Object need to be the same!!!");                   
        }             
    }
}
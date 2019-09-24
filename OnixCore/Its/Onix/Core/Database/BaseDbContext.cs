using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Its.Onix.Core.Databases
{
	public class BaseDbContext : DbContext
	{
        private static ILoggerFactory loggerFactory = null;
        private readonly DbCredential credential = null;

        public BaseDbContext(DbCredential credential)
        {
            this.credential = credential;
        }

        public DbCredential GetCredential()
        {
            return credential;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Configure(optionsBuilder);
        }

        private void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            if (credential.IsProviderPgSql())
            {
                optionsBuilder.UseNpgsql(credential.ConnectionStringPgSql());
            }
            else if (credential.IsProviderSqLiteMemory())
            {
                optionsBuilder.UseSqlite(credential.ConnectionStringSqLiteMemory());
            }            
        }

        public void TestOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            OnConfiguring(optionsBuilder);
        }

        public void SetLoggerFactory(ILoggerFactory lg)
        {
            loggerFactory = lg;
        }        
    }
}

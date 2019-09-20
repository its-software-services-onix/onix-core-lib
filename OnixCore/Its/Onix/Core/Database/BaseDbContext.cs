using System;
using Microsoft.EntityFrameworkCore;

namespace Its.Onix.Core.Databases
{
	public class BaseDbContext : DbContext
	{
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
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (credential.IsProviderPgSql())
            {
                optionsBuilder.UseNpgsql(credential.ConnectionStringPgSql());
            }
        }
    }
}

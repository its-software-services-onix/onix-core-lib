using System;

namespace Its.Onix.Core.Databases
{
    public class MockedDbContext : BaseDbContext
    {
        public MockedDbContext(DbCredential credential) : base (credential)
        {
        }
    }
}

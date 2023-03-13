using Formula1API.TestsFixture;
using Xunit;

namespace Formula1API.Tests
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<InMemoryDatabaseFixture>
    {
    }
}

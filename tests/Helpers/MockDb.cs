using Microsoft.EntityFrameworkCore;
using backend_challenge.context;

namespace UnitTests.Helpers;

public class MockDb : IDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{Guid.NewGuid()}")
            .Options;
        return new AppDbContext(options);
    }
}
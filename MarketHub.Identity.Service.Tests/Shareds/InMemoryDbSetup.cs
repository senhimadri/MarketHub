using MarketHub.IdentityModule.Api;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.IdentityModule.UnitTest.Shareds;

internal static class InMemoryDbSetup
{
    public static AppDbContext CreateInMemoryDbContext()
    {
        var option = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

        return new AppDbContext(option);
    }
}

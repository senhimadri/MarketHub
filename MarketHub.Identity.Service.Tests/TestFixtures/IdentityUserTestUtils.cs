using MarketHub.IdentityModule.Api;
using MarketHub.IdentityModule.UnitTest.TestFixtures.TestDatas;

namespace MarketHub.IdentityModule.UnitTest.TestFixtures;

internal class IdentityUserTestUtils(AppDbContext context)
{
	private readonly AppDbContext _context = context;

	public async Task AddDefaultValue()
	{
        _context.IdentityUsers.Add(IdentityUserFactory.CreateDefaultUser());
        await _context.SaveChangesAsync();
    }
}

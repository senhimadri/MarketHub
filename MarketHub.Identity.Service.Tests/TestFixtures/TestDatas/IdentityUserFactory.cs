using MarketHub.IdentityModule.Api.Entities;

namespace MarketHub.IdentityModule.UnitTest.TestFixtures.TestDatas;

internal static class IdentityUserFactory
{
    public static IdentityUser CreateDefaultUser(string username = "testuser", string password = "password")
    {
        return new IdentityUser
        {
            Id = Guid.NewGuid(),
            UserName = username,
            PasswordHash = password,
            IsActive = true,
            IsDeleted = false,
            RefreshToken = null,
            RefreshTokenExpiry = DateTime.UtcNow.AddDays(7)
        };
    }

    public static IdentityUser CreateUserWithExpiredRefreshToken(string username = "testuser")
    {
        var user = CreateDefaultUser(username);
        user.RefreshToken = "expired_refresh_token";
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(-1);
        return user;
    }
}

using MarketHub.IdentityModule.Api;
using MarketHub.IdentityModule.Api.Repositories.Login;
using MarketHub.IdentityModule.UnitTest.Shareds;
using MarketHub.IdentityModule.UnitTest.TestFixtures.Mockings;
using MarketHub.IdentityModule.UnitTest.TestFixtures.TestDatas;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.IdentityModule.UnitTest.Services;

public class LoginServiceTests
{
    private readonly MockTokenUtils _mockTokenUtils;
    private readonly AppDbContext _context;
    private readonly ILoginService _loginServices;

    public LoginServiceTests()
    {
        _context = InMemoryDbSetup.CreateInMemoryDbContext();

        _context.IdentityUsers.Add(IdentityUserFactory.CreateDefaultUser());
        _context.SaveChanges();

        _mockTokenUtils = new MockTokenUtils();

        _loginServices = new LoginService(_context, _mockTokenUtils._tokenUtilsMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnTokenResponse_WhenCredentialsAreValied()
    {
        var payload = LoginDtoFactory.CreateValidLoginDto();

        var response = await _loginServices.LoginAsync(payload);

        Assert.NotNull(response);
        Assert.Equal("access_token", response.AccessToken);
        Assert.Equal("refresh_token", response.RefreshToken);

        var user = await _context.IdentityUsers
                        .FirstOrDefaultAsync(u => u.UserName == payload.Username
                                            && u.PasswordHash == payload.Password
                                            && u.IsActive && !u.IsDeleted);

        Assert.NotNull(user);
        Assert.Equal("refresh_token", user.RefreshToken);
        Assert.True(user.RefreshTokenExpiry > DateTime.UtcNow);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedAccessException_WhenCredentialsAreNotValidate()
    {
        var loginDto = LoginDtoFactory.CreateInvalidLoginDto();
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
                        () => _loginServices.LoginAsync(loginDto));
    }


    [Fact]
    public async Task RefreshTokenAsync_ShouldReturnNewTokenAndRefreshToken_WhenRefreshTokenIsValid()
    {

        var user = await _context.IdentityUsers.FirstOrDefaultAsync(u => u.UserName == TestConstants.DefaultUsername);

        user!.RefreshToken = "valid_refresh_token";
        user!.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();

        var response = await _loginServices.RefreshTokenAsync("valid_refresh_token");


        Assert.NotNull(response);
        Assert.Equal("access_token", response.AccessToken);
        Assert.Equal("refresh_token", response.RefreshToken);

        var updatedUser = await _context.IdentityUsers
                        .FirstOrDefaultAsync(u => u.UserName == "testuser");

        Assert.NotNull(user);
        Assert.Equal("refresh_token", user.RefreshToken);
        Assert.True(user.RefreshTokenExpiry > DateTime.UtcNow);
    }


    [Fact]
    public async Task RefreshTokenAsync_ShouldThrowInvalidOperationException_WhenRefreshTokenIsInValid()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(
                () => _loginServices.RefreshTokenAsync("invalid_refreshToken"));
    }


    [Fact]
    public async Task RefreshTokenAsync_ShouldThrowInvalidOperationException_WhenRefreshTokenIsInExpired()
    {
        var user = await _context.IdentityUsers.FirstOrDefaultAsync(x => x.UserName == "testuser");

        user!.RefreshTokenExpiry = DateTime.UtcNow.AddDays(-1);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _loginServices.RefreshTokenAsync("refresh_token"));

        user!.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();
    }
}

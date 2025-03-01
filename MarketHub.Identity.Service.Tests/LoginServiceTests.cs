using MarketHub.IdentityModule.Api.DataTransferObjects;
using MarketHub.IdentityModule.Api.Entities;
using MarketHub.IdentityModule.Api.Repositories.Login;
using MarketHub.IdentityModule.Api.Repositories.Token;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace MarketHub.IdentityModule.Api.Tests;

public class LoginServiceTests
{
    private readonly Mock<ITokenUtils> _tokenUtilsMock;
    private readonly AppDbContext _context;
    private readonly ILoginService _loginServices;

    public LoginServiceTests()
    {
        var options  = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _context = new AppDbContext(options);

        _context.IdentityUsers.Add(new IdentityUser
        {
            Id = Guid.NewGuid(),
            UserName = "testuser",
            PasswordHash = "password",
            IsActive = true
        });

        _context.SaveChanges();

        _tokenUtilsMock = new Mock<ITokenUtils>(MockBehavior.Strict);

        _tokenUtilsMock.Setup(x=>x.GenerateAccessToken(It.IsAny<IdentityUser>()))
                                    .Returns("access_token");

        _tokenUtilsMock.Setup(x => x.GenerateRefreshToken(It.IsAny<Guid>()))
                                    .Returns("refresh_token");

        _loginServices = new LoginService(_context, _tokenUtilsMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnTokenResponse_WhenCredentialsAreValied()
    {
        var payload = new LoginDto("testuser", "password");

        var response = await _loginServices.LoginAsync(payload);

        Assert.NotNull(response);
        Assert.Equal("access_token", response.AccessToken);
        Assert.Equal("refresh_token" , response.RefreshToken);

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
        var loginDto = new LoginDto(Username:"invaliduser", Password:"invalidpassword");
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
                        ()=> _loginServices.LoginAsync(loginDto));
    }


    [Fact]
    public async Task RefreshTokenAsync_ShouldReturnNewTokenAndRefreshToken_WhenRefreshTokenIsValid()
    {

        var user = await _context.IdentityUsers.FirstOrDefaultAsync(u=>u.UserName== "testuser");

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
        var user = await _context.IdentityUsers.FirstOrDefaultAsync(x=>x.UserName== "testuser");

        user!.RefreshTokenExpiry = DateTime.UtcNow.AddDays(-1);
        await _context.SaveChangesAsync();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _loginServices.RefreshTokenAsync("refresh_token"));

        user!.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync();
    }
}

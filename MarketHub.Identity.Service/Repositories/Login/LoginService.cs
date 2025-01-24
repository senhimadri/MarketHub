using MarketHub.Identity.Service.DataTransferObjects;
using MarketHub.Identity.Service.Repositories.Token;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Identity.Service.Repositories.Login;

public class LoginService(AppDbContext context, ITokenUtils tokenUtils) : ILoginService
{
    private readonly AppDbContext _context = context;
    private readonly ITokenUtils _tokenUtils = tokenUtils;

    public async Task<TokenResponseDto> LoginAsync(LoginDto request)
    {
        var user = await _context.IdentityUsers
                    .SingleOrDefaultAsync(u => u.UserName == request.Username
                                            && u.PasswordHash == request.Password
                                            && u.IsActive && !u.IsDeleted);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = _tokenUtils.GenerateAccessToken(user);
        var refreshToken = _tokenUtils.GenerateRefreshToken(user.Id);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return new TokenResponseDto(token, refreshToken);
    }

    public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var user = await _context.IdentityUsers
                    .SingleOrDefaultAsync(u => u.RefreshToken == refreshToken
                                            && u.RefreshTokenExpiry > DateTime.UtcNow
                                            && u.IsActive && !u.IsDeleted);

        if (user is null)
            throw new InvalidOperationException("Invalid or expired refresh token.");

        var token = _tokenUtils.GenerateAccessToken(user);
        var newRefreshToken = _tokenUtils.GenerateRefreshToken(user.Id);

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _context.SaveChangesAsync();

        return new TokenResponseDto(token, newRefreshToken);
    }
}

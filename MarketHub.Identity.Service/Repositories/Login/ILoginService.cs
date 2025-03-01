using MarketHub.IdentityModule.Api.DataTransferObjects;

namespace MarketHub.IdentityModule.Api.Repositories.Login;

public interface ILoginService
{
    Task<TokenResponseDto> LoginAsync(LoginDto request);
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
}

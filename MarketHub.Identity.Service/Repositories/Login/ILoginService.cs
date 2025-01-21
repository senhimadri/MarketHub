using MarketHub.Identity.Service.DataTransferObjects;

namespace MarketHub.Identity.Service.Repositories.Login;

public interface ILoginService
{
    Task<TokenResponseDto> LoginAsync(LoginDto request);
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
}

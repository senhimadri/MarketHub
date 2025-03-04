using MarketHub.IdentityModule.Api.Entities;

namespace MarketHub.IdentityModule.Api.Repositories.Token;

public interface ITokenUtils
{
    string GenerateAccessToken(IdentityUser user);
    string GenerateRefreshToken(Guid userId);
}

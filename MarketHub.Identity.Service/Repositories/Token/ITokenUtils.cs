using MarketHub.Identity.Service.Entities;

namespace MarketHub.Identity.Service.Repositories.Token;

public interface ITokenUtils
{
    string GenerateAccessToken(IdentityUser user);
    string GenerateRefreshToken(Guid userId);
}

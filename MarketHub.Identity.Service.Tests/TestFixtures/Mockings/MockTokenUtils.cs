using MarketHub.IdentityModule.Api.Entities;
using MarketHub.IdentityModule.Api.Repositories.Token;
using Moq;

namespace MarketHub.IdentityModule.UnitTest.TestFixtures.Mockings;

internal class MockTokenUtils
{
    public readonly Mock<ITokenUtils> _tokenUtilsMock;

    public MockTokenUtils()
    {
        _tokenUtilsMock = new Mock<ITokenUtils>(MockBehavior.Strict);
        _tokenUtilsMock.Setup(x => x.GenerateAccessToken(It.IsAny<IdentityUser>()))
                            .Returns("access_token");

        _tokenUtilsMock.Setup(x => x.GenerateRefreshToken(It.IsAny<Guid>()))
                                    .Returns("refresh_token");

    }

    public MockTokenUtils WithAccessToken(string accessToken)
    {
        _tokenUtilsMock.Setup(x => x.GenerateAccessToken(It.IsAny<IdentityUser>()))
                    .Returns(accessToken);

        return this;
    }

    public MockTokenUtils WithRefreshToken(string refreshToken)
    {
        _tokenUtilsMock.Setup(x => x.GenerateRefreshToken(It.IsAny<Guid>()))
                            .Returns(refreshToken);

        return this;
    }
}

using MarketHub.IdentityModule.Api.DataTransferObjects;

namespace MarketHub.IdentityModule.UnitTest.TestFixtures.TestDatas;

internal static class LoginDtoFactory
{
    public static LoginDto CreateValidLoginDto(string username = "testuser", string password = "password")
    {
        return new LoginDto(username, password);
    }

    public static LoginDto CreateInvalidLoginDto()
    {
        return new LoginDto("invaliduser", "invalidpassword");
    }
}

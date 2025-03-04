namespace MarketHub.IdentityModule.Api.DataTransferObjects
{
    public record LoginDto(string Username, string Password);
    public record TokenResponseDto(string AccessToken, string RefreshToken);
}

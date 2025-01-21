namespace MarketHub.Identity.Service.DataTransferObjects
{
    public record LoginDto(string Username, string Password);
    public record TokenResponseDto(string AccessToken, string RefreshToken);
}

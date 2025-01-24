namespace MarketHub.Identity.Service.Entities;

public class IdentityUser
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? Email { get; set; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }


}
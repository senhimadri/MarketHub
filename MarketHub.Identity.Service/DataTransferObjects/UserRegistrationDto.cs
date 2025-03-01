namespace MarketHub.IdentityModule.Api.DataTransferObjects;

public record UserRegistrationDto(Guid Id, string UserName, string PasswordHash, string Email);
public record BaseIdentityUserDto(string UserName, string PasswordHash, string Email);

public record CreateIdentityUserDto(string UserName, string PasswordHash, string Email) 
                                        : BaseIdentityUserDto(UserName, PasswordHash, Email);

public record UpdateIdentityUserDto(Guid Id ,string UserName, string PasswordHash, string Email)
                                        : BaseIdentityUserDto(UserName, PasswordHash, Email);
using MarketHub.Identity.Service.DataTransferObjects;

namespace MarketHub.Identity.Service.Repositories.Registration;

public interface IRegistrationService
{
    Task RegisterUserAsync(CreateIdentityUserDto user);
}

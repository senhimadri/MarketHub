using MarketHub.Common.Library.OperationResult;
using MarketHub.Identity.Service.DataTransferObjects;

namespace MarketHub.Identity.Service.Repositories.Registration;

public interface IRegistrationService
{
    Task<OperationResult> RegisterUserAsync(CreateIdentityUserDto user);
}

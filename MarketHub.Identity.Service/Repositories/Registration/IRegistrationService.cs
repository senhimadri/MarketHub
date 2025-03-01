using MarketHub.Common.Library.OperationResult;
using MarketHub.IdentityModule.Api.DataTransferObjects;

namespace MarketHub.IdentityModule.Api.Repositories.Registration;

public interface IRegistrationService
{
    Task<OperationResult> RegisterUserAsync(CreateIdentityUserDto user);
}

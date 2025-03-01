using MarketHub.Common.Library.OperationResult;
using MarketHub.IdentityModule.Api.DataTransferObjects;
using MarketHub.IdentityModule.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.IdentityModule.Api.Repositories.Registration;

public class RegistrationService(AppDbContext context) : IRegistrationService
{
    private readonly AppDbContext _context = context;

    public async Task<OperationResult> RegisterUserAsync(CreateIdentityUserDto user)
    {
        if (await _context.IdentityUsers.AnyAsync(x => x.UserName == user.UserName))
            return Errors.NewError(400, "Already Exist");

        var newUser = new IdentityUser
        {
            Id = Guid.NewGuid(),
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            Email = user.Email,
            IsActive = true,
            IsDeleted = false
        };

        _context.IdentityUsers.Add(newUser);
        await _context.SaveChangesAsync();

        return OperationResult.Success();
    }
}

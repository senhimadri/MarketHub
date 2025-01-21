using MarketHub.Identity.Service.DataTransferObjects;
using MarketHub.Identity.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Identity.Service.Repositories.Registration;

public class RegistrationService(AppDbContext context) : IRegistrationService
{
    private readonly AppDbContext _context = context;

    public async Task RegisterUserAsync(CreateIdentityUserDto user)
    {
        if (await _context.IdentityUsers.AnyAsync(x => x.UserName == user.UserName))
            throw new Exception("Username already exists.");

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
    }
}

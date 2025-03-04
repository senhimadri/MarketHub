using MarketHub.IdentityModule.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.IdentityModule.Api;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<IdentityUser> IdentityUsers { get; set; }
}

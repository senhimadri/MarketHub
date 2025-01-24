using MarketHub.Identity.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Identity.Service;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<IdentityUser> IdentityUsers { get; set; }
}

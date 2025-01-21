using MarketHub.Identity.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Identity.Service;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<IdentityUser> IdentityUsers { get; set; }
}

using MarketHub.ProductModule.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MarketHub.ProductModule.Api;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemCategory>()
            .HasKey(ic => new { ic.ItemId, ic.CategoryId });
        
    }
    public DbSet<Item> Item { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<ItemCategory> ItemCategory { get; set; } = null!;
    public DbSet<ItemPriceLog> ItemPriceLog { get; set; } = null!;

}

public static class ModelBuilderExtensions
{
    public static void ApplyGlobalFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var filter = Expression.Lambda(
                           Expression.Equal(property, Expression.Constant(false)), // e.IsDeleted == false
                           parameter
                       );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }
}


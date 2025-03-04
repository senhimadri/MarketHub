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
        modelBuilder.ApplyGlobalFilters();
    }
    public DbSet<Item> Item { get; } = null!;
    public DbSet<Category> Category { get; } = null!;
    public DbSet<ItemCategory> ItemCategory { get;  } = null!;
    public DbSet<ItemPriceLog> ItemPriceLog { get; } = null!;

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
                    Expression.Not(property),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
    }
}


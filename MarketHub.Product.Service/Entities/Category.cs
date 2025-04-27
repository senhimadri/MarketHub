using System.ComponentModel.DataAnnotations;

namespace MarketHub.ProductModule.Api.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();
}




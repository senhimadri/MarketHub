namespace MarketHub.ProductModule.Api.Entities;

public class Item : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Stock { get; set; }
    public string? ImageUrl { get; set; }
    public virtual ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();
}




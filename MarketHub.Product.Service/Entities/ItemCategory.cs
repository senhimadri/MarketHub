namespace MarketHub.Product.Service.Entities;

public class ItemCategory
{
    public Guid ItemId { get; set; } 
    public Guid CategoryId { get; set; }
    public virtual Item Item { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
}




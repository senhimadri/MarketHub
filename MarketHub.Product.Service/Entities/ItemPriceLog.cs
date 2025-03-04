namespace MarketHub.ProductModule.Api.Entities;

public class ItemPriceLog : BaseEntity
{
    public Guid ItemId { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime EffectiveFrom { get; set; }

    public virtual Item? Item { get; set; } 
}




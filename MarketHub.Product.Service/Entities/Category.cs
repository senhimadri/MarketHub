namespace MarketHub.Product.Service.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public virtual ICollection<Category> ChildCategory { get; set; } = new List<Category>();
    public virtual ICollection<ItemCategory> ItemCategorys { get; set; } = new List<ItemCategory>();
}




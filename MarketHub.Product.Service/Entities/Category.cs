using System.ComponentModel.DataAnnotations;

namespace MarketHub.ProductModule.Api.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> ChildCategories { get; set; } = new List<Category>();
    public ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();
}




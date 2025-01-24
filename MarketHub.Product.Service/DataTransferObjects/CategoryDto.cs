using MarketHub.Product.Service.Entities;

namespace MarketHub.Product.Service.DataTransferObjects;

public record CategoryDto(Guid Id, string Name);
public record CreateCategoryDto(string Name, string? Description, Guid? ParentCategoryId);

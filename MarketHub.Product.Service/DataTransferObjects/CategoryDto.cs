using MarketHub.ProductModule.Api.Entities;

namespace MarketHub.ProductModule.Api.DataTransferObjects;

public record CategoryDto(Guid Id, string Name);
public record CreateCategoryDto(string Name, string? Description, Guid? ParentCategoryId);

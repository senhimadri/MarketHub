namespace MarketHub.Product.Service.DataTransferObjects;

public record ItemBaseDto(
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    decimal Stock,
    string? ImageUrl,
    IEnumerable<Guid> CategoryIds
);

public record CreateItemDto(
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    decimal Stock,
    string? ImageUrl,
    IEnumerable<Guid> CategoryIds
) : ItemBaseDto(Name, Description, SKU, Price, Stock, ImageUrl, CategoryIds);

public record UpdateItemDto(
    Guid Id,
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    decimal Stock,
    string? ImageUrl,
    IEnumerable<Guid> CategoryIds
) : ItemBaseDto(Name, Description, SKU, Price, Stock, ImageUrl, CategoryIds);

public record GetItemDto(
    Guid Id,
    string Name,
    string? Description,
    string SKU,
    decimal Price,
    decimal Stock,
    string? ImageUrl,
    IEnumerable<CategoryDto>? Categories
);

public record GetItemPaginationDto(
    Guid Id,
    string Name,
    string SKU,
    decimal Price,
    string? ImageUrl
);

namespace MarketHub.Product.Service.Helper;

public record PaginationDto<T>(int TotalCount, List<T>? Data);

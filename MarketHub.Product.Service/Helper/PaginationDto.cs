namespace MarketHub.ProductModule.Api.Helper;

public record PaginationDto<T>(int TotalCount, List<T>? Data);

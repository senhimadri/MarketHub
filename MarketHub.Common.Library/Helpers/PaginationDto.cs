namespace MarketHub.Common.Library;

public record PaginationDto<T>(long TotalCount, List<T>? Data);

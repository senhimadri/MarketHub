using MarketHub.Common.Library.OperationResult;
using MarketHub.ProductModule.Api.DataTransferObjects;

namespace MarketHub.ProductModule.Api.Repositories.IServices;

public interface IItemRepository
{
    public Task<OperationResult> CreateItemAsync(CreateItemDto request);
    public Task<OperationResult> UpdateItemAsync(UpdateItemDto request);
    public Task<OperationResult> DeleteItemAsync(Guid id);
    public Task<GetItemDto?> GetbyItemAsync(Guid id);

}

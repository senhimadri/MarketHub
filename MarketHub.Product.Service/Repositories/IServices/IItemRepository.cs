using MarketHub.Common.Library.OperationResult;
using MarketHub.Product.Service.DataTransferObjects;

namespace MarketHub.Product.Service.Repositories.IServices;

public interface IItemRepository
{
<<<<<<< HEAD
    public interface IItemRepository
    {
        public Task<OperationResult> CreateItemAsync(CreateItemDto request);
        public Task<OperationResult> UpdateItemAsync(UpdateItemDto request);
        public Task<OperationResult> DeleteItemAsync(Guid id);
        public Task<GetItemDto> GetbyItemAsync(Guid id);
    }
=======
    public Task<OperationResult> CreateItemAsync(CreateItemDto request);
    public Task<OperationResult> UpdateItemAsync(UpdateItemDto request);
    public Task<OperationResult> DeleteItemAsync(Guid id);
    public Task<GetItemDto?> GetbyItemAsync(Guid id);
>>>>>>> docer-compose update
}

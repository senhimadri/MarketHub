using MarketHub.Common.Library.OperationResult;
using MarketHub.ProductModule.Api.DataTransferObjects;
using MarketHub.ProductModule.Api.Entities;
using MarketHub.ProductModule.Api.Repositories.IServices;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.ProductModule.Api.Repositories.Services;

public class ItemRepository(AppDbContext context) : IItemRepository
{
    private readonly AppDbContext _context = context;

    public async Task<OperationResult> CreateItemAsync(CreateItemDto request)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        var itemId = Guid.NewGuid();

        var newItem = new Item
        {
            Id = itemId,
            Name = request.Name,
            Description = request.Description,
            SKU = request.SKU,
            Price = request.Price,
            Stock = request.Stock,
            ImageUrl = request.ImageUrl,
            CreatedAt = DateTime.UtcNow
        };
        _context.Add(newItem);
        await _context.SaveChangesAsync();

        var newItemCategory = request.CategoryIds.Select(CategoryId => new ItemCategory
        {
            ItemId = itemId,
            CategoryId = CategoryId
        }).ToList();

        _context.ItemCategory.AddRange(newItemCategory);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return OperationResult.Success();
 
    }

    public async Task<OperationResult> UpdateItemAsync(UpdateItemDto request)
    {
        var existingItem = await _context.Item.FindAsync(request.Id);
        if (existingItem is null)
            return Errors.ContentNotFound;

        existingItem.Name = request.Name;
        existingItem.Description = request.Description;
        existingItem.SKU = request.SKU;
        existingItem.Price = request.Price;
        existingItem.Stock = request.Stock;
        existingItem.ImageUrl = request.ImageUrl;
        existingItem.UpdatedAt = DateTime.UtcNow;

        _context.Update(existingItem);

        var existingItemCategories = await _context.ItemCategory
                            .Where(x => x.ItemId == request.Id)
                            .ToListAsync();

        var existingCategoryIds = existingItemCategories.Select(x => x.CategoryId).ToHashSet();
        var newCategoryIds = request.CategoryIds.ToHashSet();

        var itemCategoriesToAdd = newCategoryIds.Except(existingCategoryIds).Select(categoryId => new ItemCategory
        {
            ItemId = request.Id,
            CategoryId = categoryId
        }).ToList();

        var itemCategoriesToRemove = existingItemCategories
                .Where(x => !newCategoryIds.Contains(x.CategoryId))
                .ToList();

        _context.ItemCategory.RemoveRange(itemCategoriesToRemove);
        _context.ItemCategory.AddRange(itemCategoriesToAdd);

        await _context.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteItemAsync(Guid id)
    {
        var existingItem = await _context.Item.FindAsync(id);

        if (existingItem is null)
            return Errors.ContentNotFound;

        existingItem.IsDeleted = true;

        await _context.SaveChangesAsync();


        return OperationResult.Success();
    }

    public async Task<GetItemDto?> GetbyItemAsync(Guid id)
    {
        var item = await _context.Item
            .Include(x => x.ItemCategories)
            .ThenInclude(ic => ic.Category)
            .Where(x => x.Id == id && x.IsDeleted == false)
            .Select(x => new GetItemDto(
                x.Id,
                x.Name,
                x.Description,
                x.SKU,
                x.Price,
                x.Stock,
                x.ImageUrl,
                x.ItemCategories.Select(category => new CategoryDto
                (
                    category.Category.Id,
                    category.Category.Name
                )).ToList()
            ))
            .FirstOrDefaultAsync();

        return item;
    }
}

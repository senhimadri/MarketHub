using MarketHub.Identity.Service;
using MarketHub.Product.Service.DataTransferObjects;
using MarketHub.Product.Service.Entities;
using MarketHub.Product.Service.Helper;
using MarketHub.Product.Service.Repositories.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketHub.Product.Service.Endpoints;

public static class ItemEndpoints
{
    public static void MapItemEndpoints(this WebApplication app)
    {
        app.MapPost("/create-item", async (IItemRepository _ItemService, [FromBody] CreateItemDto request) =>
        {
            var response = await _ItemService.CreateItemAsync(request);

            return response.Match(onSuccess: () => Results.Created(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        });

        app.MapPut("/update-item", async (IItemRepository _ItemService, [FromBody] UpdateItemDto request) =>
        {
            var response = await _ItemService.UpdateItemAsync(request);

            return response.Match(onSuccess: () => Results.NoContent(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description)); 
        });

        app.MapDelete("/delete-item", async (IItemRepository _ItemService, Guid id) =>
        {
            var response = await _ItemService.DeleteItemAsync(id);

            return response.Match(onSuccess: () => Results.NoContent(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        });

        app.MapGet("/getby-item", async (IItemRepository _ItemService, Guid id) =>
        {
            var response = await _ItemService.GetbyItemAsync(id);

            return response.Match(onSuccess: () => Results.NoContent(),
                        onValidationFailure: (validationErrors) => Results.ValidationProblem(validationErrors),
                        onFailure: (error) => Results.Problem(error.Description));
        });

        app.MapGet("/pagination-item", async (AppDbContext _context, Guid? categoryId, string? searchText, int pageNo, int size, bool isPaginated) =>
        {
            var query = _context.ItemCategory
                        .Include(item => item.Item)
                        .Include(catecory => catecory.Category)
                        .Where(x => (categoryId == null || x.CategoryId == categoryId)
                                    && !x.Item.IsDeleted && !x.Category.IsDeleted);

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(x => x.Category.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                        x.Item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }

            if (isPaginated)
            {
                pageNo = pageNo < 1 ? 1 : pageNo;
                size = size < 1 ? 10 : size;
                query = query.Skip((pageNo - 1) * size).Take(size);
            }

            var data = await query
                            .Select(x => new GetItemPaginationDto(
                                x.Item.Id,
                                x.Item.Name,
                                x.Item.SKU,
                                x.Item.Price,
                                x.Item.ImageUrl))
                            .ToListAsync();

            var response = new PaginationDto<GetItemPaginationDto>(totalCount, data);

            return Results.Ok(response);
        });
    }
}

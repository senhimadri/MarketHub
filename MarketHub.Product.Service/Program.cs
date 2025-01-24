using MarketHub.Product.Service;
using MarketHub.Product.Service.DataTransferObjects;
using MarketHub.Product.Service.Entities;
using MarketHub.Product.Service.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/create-item", async (AppDbContext _context,[FromBody]CreateItemDto request) =>
{
    var itemId = Guid.NewGuid();

    var newItem = new Item
    {
        Id = itemId,
        Name = request.Name ,
        Description = request.Description ,
        SKU  = request.SKU  ,
        Price = request.Price ,
        Stock = request.Stock ,
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

    return Results.Created();

});

app.MapPut("/update-item", async (AppDbContext _context, [FromBody] UpdateItemDto request) =>
{

    var existingItem = await _context.Item.FindAsync(request.Id);
    if (existingItem is null)
        return Results.BadRequest("Item not found.");

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

    return Results.NoContent();
});

app.MapDelete("/delete-item", async (AppDbContext _context, Guid id) =>
{
    var existingItem = await _context.Item.FindAsync(id);

    if (existingItem is null)
        return Results.BadRequest("Item is not available");

    existingItem.IsDeleted = true;

    await _context.SaveChangesAsync();

    return Results.Ok("Deleted Successfully.");
});

app.MapGet("/getby-item", async(AppDbContext _context, Guid id)=>
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


    if (item is null)
        return Results.NotFound("Item not found.");

    return Results.Ok(item);
});

app.MapGet("/pagination-item", async (AppDbContext _context, Guid? categoryId,string? searchText, int pageNo, int size, bool isPaginated) =>
{
    var query = _context.ItemCategory
                .Include(item => item.Item)
                .Include(catecory => catecory.Category)
                .Where(x => (categoryId == null || x.CategoryId == categoryId) 
                            && !x.Item.IsDeleted && !x.Category.IsDeleted);

    var totalCount = await query.CountAsync();

    if (!String.IsNullOrEmpty(searchText))
    {
        searchText=searchText.ToLower();
        query = query.Where(x => x.Category.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) || 
                                x.Item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }

    if (isPaginated)
    {
        pageNo = pageNo < 1 ? 1 : pageNo;
        size = size < 1 ? 10 : size;
        query = query.Skip(pageNo - 1 * size).Take(size);
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

app.Run();
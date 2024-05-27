using System.Net;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.DTOs;
using Web.Data.Entities;
using WebApi.Data.Response;

namespace Web.Services.CateGoryService;

public class CategoryService : ICategoryService
{
    #region ctor

    private readonly DataContext _context;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(DataContext context, ILogger<CategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }

    #endregion
    
    #region GetCategoriesAsync

    public async Task<Response<List<GetCategoryDto>>> GetCategoriesAsync()
    {
        try
        {
            _logger.LogInformation("Starting method GetCategoriesAsync in time: {DateTimeNow}", DateTime.Now.Millisecond);
            var categories = await _context.Categories.Select(x => new GetCategoryDto()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToListAsync();
            _logger.LogInformation("Finished method GetCategoriesAsync in time: {DateTimeNow}", DateTime.Now.Millisecond);

            return new Response<List<GetCategoryDto>>(categories);
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting in method GetCategoriesAsync in time: {DateTimeNow}\n,error:{EMessage}",
                DateTime.Now, e.Message);
            return new Response<List<GetCategoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region GetCategoryByIdAsync

    public async Task<Response<GetCategoryDto>> GetCategoryByIdAsync(int categoryId)
    {
        try
        {
            _logger.LogInformation("Starting method GetCategoryByIdAsync in time: {DateTimeNow}", DateTime.Now);
            var existingCategory = await _context.Categories.Where(x => x.Id == categoryId).Select(x =>
                new GetCategoryDto()
                {
                    Name = x.Name,
                    Id = x.Id
                }).FirstOrDefaultAsync();
            if (existingCategory == null)
            {
                _logger.LogWarning("Not found category with={Id},in time {DateTimeNow}", categoryId, DateTime.Now);
                return new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Category not found");
            }

            _logger.LogInformation("Finished method GetCategoryByIdAsync in time: {DateTimeNow}", DateTime.Now);
            return new Response<GetCategoryDto>(existingCategory);
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting in method GetCategoriesAsync in time: {DateTimeNow}\n,error:{EMessage}",
                DateTime.Now, e.Message);
            return new Response<GetCategoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region CreateCategoryAsync

    public async Task<Response<string>> CreateCategoryAsync(CreateCategoryDto category)
    {
        try
        {
            _logger.LogInformation("Starting method CreateCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            var existingCategory = await _context.Categories.AnyAsync(x => x.Name == category.Name);
            if (existingCategory)
            {
                _logger.LogWarning("Category already exists with={Name},in time {DateTimeNow}", category.Name, DateTime.Now);
                return new Response<string>(HttpStatusCode.BadRequest, "Category already exists");
            }

            var newCategory = new Category
            {
                Name = category.Name,
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Finished method CreateCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            return new Response<string>("Successfully created category");
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting in method CreateCategoryAsync in time: {DateTimeNow}\n,error:{EMessage}",
                DateTime.Now, e.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion

    #region UpdateCategoryAsync

    public async Task<Response<string>> UpdateCategoryAsync(UpdateCategoryDto category)
    {
        try
        {
            _logger.LogInformation("Starting method UpdateCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            var existingCategory = await _context.Categories.Where(x => x.Id == category.Id)
                .ExecuteUpdateAsync(x => x.SetProperty(c => c.Name, category.Name));
            if (existingCategory == 0)
            {
                _logger.LogWarning("Not found category with={Id},in time {DateTimeNow}", category.Id, DateTime.Now);
                return new Response<string>(HttpStatusCode.BadRequest, $"Not found category with Id={category.Id}");
            }
            
            _logger.LogInformation("Finished method UpdateCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            return new Response<string>("Successfully updated category");
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting in method UpdateCategoryAsync in time: {DateTimeNow}\n,error:{EMessage}",
                DateTime.Now, e.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    

    #endregion

    #region DeleteCategoryAsync

    public async Task<Response<bool>> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            _logger.LogInformation("Starting method DeleteCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            var existingCategory = await _context.Categories.Where(x => x.Id == categoryId).ExecuteDeleteAsync();
               
            if (existingCategory == 0)
            {
                _logger.LogWarning("Not found category with={Id},in time {DateTimeNow}", categoryId, DateTime.Now);
                return new Response<bool>(HttpStatusCode.BadRequest, "Category not found");
            }

            _logger.LogInformation("Finished method DeleteCategoryAsync in time: {DateTimeNow}", DateTime.Now);
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError("Error getting in method DeleteCategoryAsync in time: {DateTimeNow}\n,error:{EMessage}",
                DateTime.Now, e.Message);
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    #endregion
}
using BlogApp.BL.DTOs.Categories;

namespace BlogApp.BL.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, CategoryUpdateDto dto);
}

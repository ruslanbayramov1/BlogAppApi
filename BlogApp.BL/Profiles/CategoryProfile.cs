using AutoMapper;
using BlogApp.BL.DTOs.Categories;
using BlogApp.Core.Entities;

namespace BlogApp.BL.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryGetDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
    }
}

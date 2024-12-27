using AutoMapper;
using BlogApp.BL.DTOs.Categories;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.BL.Services.Implements;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        var entity = _mapper.Map<Category>(dto);
        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        await _isExists(id);

        var entity = await _repository.GetByIdAsync(id);
        await Task.Run(() => _repository.Delete(entity!));
        await _repository.SaveAsync();
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAll().ToListAsync();
        var data = _mapper.Map<List<CategoryGetDto>>(entities);
        return data;
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        await _isExists(id);

        var entity = await _repository.GetByIdAsync(id);
        var data = _mapper.Map<CategoryGetDto>(entity);
        return data;
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto dto)
    {
        await _isExists(id);

        var entity = await _repository.GetByIdAsync(id);
        _mapper.Map(dto, entity);
        await _repository.SaveAsync();
    }

    private async Task _isExists(int id)
    {
        bool res = await _repository.IsExistsAsync(id);
        if (!res) throw new NotFoundException<Category>();
    }
}

using AutoMapper;
using BlogApp.BL.DTOs.Users;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
using BlogApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.BL.Services.Implements;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> RegisterAsync(UserCreateDto dto)
    {
        var entity = _mapper.Map<User>(dto);
        entity.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);
        entity.Role = (int)Roles.Publisher;

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();

        return entity.Id;
    }

    public async Task LoginAsync(UserLoginDto dto)
    {
        User? user = await _repository.GetByUsernameAsync(dto.UserName);
        if (user == null) throw new NotFoundException<User>("Username or password is wrong");

        bool res = BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash);
        if (!res) throw new NotFoundException<User>("Username or password is wrong");
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserGetDto>> GetAllAsync()
    {
        var entities = await _repository.GetAll().ToListAsync();
        var data = _mapper.Map<List<UserGetDto>>(entities);
        return data;
    }

    public async Task<UserGetDto> GetByIdAsync(int id)
    {
        await _isExists(id);

        var entity = await _repository.GetByIdAsync(id);
        var data = _mapper.Map<UserGetDto>(entity);
        return data;
    }

    public Task UpdateAsync(int id, UserUpdateDto dto)
    {
        throw new NotImplementedException();
    }
    private async Task _isExists(int id)
    {
        bool res = await _repository.IsExistsAsync(id);
        if (!res) throw new NotFoundException<User>();
    }
}

using AutoMapper;
using BlogApp.BL.DTOs.Users;
using BlogApp.BL.Exceptions;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
using BlogApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.BL.Services.Implements;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;
    private readonly HttpContext _httpContext;
    private readonly ICacheService _cacheService;
    public UserService(IUserRepository repository, IMapper mapper, IJwtService jwtService, IEmailService emailService, IHttpContextAccessor httpContext, ICacheService cacheService)
    {
        _repository = repository;
        _mapper = mapper;
        _jwtService = jwtService;
        _emailService = emailService;
        _httpContext = httpContext.HttpContext!;
        _cacheService = cacheService;
    }

    public async Task<int> RegisterAsync(UserCreateDto dto)
    {
        if (await _repository.GetByUsernameAsync(dto.UserName) != null)
        {
            throw new ExistsException<User>();
        }

        if (await _repository.GetByEmailAsync(dto.Email) != null)
        {
            throw new ExistsException<User>();
        }

        var entity = _mapper.Map<User>(dto);
        entity.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);
        entity.Role = (int)Roles.Viewer;

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();

        return entity.Id;
    }

    public async Task<string> LoginAsync(UserLoginDto dto)
    {
        User? user = await _repository.GetByUsernameAsync(dto.UserName);
        if (user == null) throw new NotFoundException<User>("Username or password is wrong");

        bool res = BCrypt.Net.BCrypt.EnhancedVerify(dto.Password, user.PasswordHash);
        if (!res) throw new NotFoundException<User>("Username or password is wrong");

        string token = _jwtService.CreateToken(user, 24);

        return token;
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

    public async Task SendVerifyEmailAsync()
    {
        var userName = _httpContext.User.FindFirst(ClaimTypes.Name);
        var email = _httpContext.User.FindFirst(ClaimTypes.Email);

        if (String.IsNullOrWhiteSpace(userName?.ToString()) || String.IsNullOrWhiteSpace(email?.ToString()))
        {
            throw new NotFoundException<User>();
        }

        Guid guid = Guid.NewGuid();
        await _cacheService.Set(userName.Value + "_emailVerify", guid.ToString());

        await _emailService.SendEmailVerificationAsync(email.Value, userName.Value, guid.ToString());
    }

    public async Task VerifyEmail(string user, string code)
    {
        string? resCode = await _cacheService.Get<string>(user + "_emailVerify");

        if (resCode != code) throw new InvalidTokenException();
        
        User? u = await _repository.GetByUsernameAsync(user);
        if (u == null) throw new NotFoundException<User>();

        u.Role = (int)Roles.Publisher;
        await _repository.SaveAsync();
    }
}

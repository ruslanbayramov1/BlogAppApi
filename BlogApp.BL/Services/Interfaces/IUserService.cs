using BlogApp.BL.DTOs.Users;

namespace BlogApp.BL.Services.Interfaces;

public interface IUserService
{
    Task<List<UserGetDto>> GetAllAsync();
    Task<UserGetDto> GetByIdAsync(int id);
    Task<int> RegisterAsync(UserCreateDto dto);
    Task<string> LoginAsync(UserLoginDto dto);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, UserUpdateDto dto);
}

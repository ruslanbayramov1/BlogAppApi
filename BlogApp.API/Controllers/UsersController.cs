using BlogApp.BL.DTOs.Users;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    { 
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register(UserCreateDto dto)
    { 
        int res = await _service.RegisterAsync(dto);
        return StatusCode(StatusCodes.Status201Created, res);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        string token = await _service.LoginAsync(dto);
        return Ok(token);
    }
}

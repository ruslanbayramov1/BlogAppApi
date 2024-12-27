using BlogApp.BL.DTOs.Categories;
using BlogApp.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;    
    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _service.GetByIdAsync(id);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CategoryCreateDto dto)
    {
        int res = await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created, res);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return Created();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    { 
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

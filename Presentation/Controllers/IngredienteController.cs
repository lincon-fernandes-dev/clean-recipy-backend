using Application.DTOs;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class IngredientController(IIngredientService userService) : ControllerBase
{
    private readonly IIngredientService _ingredientService = userService;

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAll()
    {
        var ingredient = await _ingredientService.GetAllAsync();
        if (ingredient == null)
            return NoContent();
        return Ok(ingredient);
    }
    [HttpGet("GetIngredientsByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        var ingredient = await _ingredientService.GetByNameAsync(name);
        if (ingredient == null) return NoContent();
        return Ok(ingredient);
    }
    [HttpPost("CreateIngredient")]
    public async Task<IActionResult> Create(IngredientDTO dto)
    {
        var newIngredient = await _ingredientService.CreateAsync(dto);
        if (newIngredient == null)
            return NotFound();
        return Ok(newIngredient);
    }
    [HttpPost]
    public async Task<IActionResult> Update(IngredientDTO dto)
    {
        var newIngredient = await _ingredientService.UpdateAsync(dto);
        if (newIngredient == null)
            return NotFound();
        return Ok(newIngredient);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(IngredientDTO dto)
    {
        var deletedIngredient = await _ingredientService.DeleteAsync(dto.Id);
        
        return Ok(deletedIngredient);
    }
}


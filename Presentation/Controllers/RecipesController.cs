using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipes()
    {
        var recipes = await _recipeService.GetAllRecipes();
        return Ok(recipes);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
    {
        var recipe = await _recipeService.GetById(id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
    [HttpPost]
    public async Task<ActionResult<RecipeDTO>> CreateRecipe(CreateRecipeDTO recipeDTO)
    {
        await _recipeService.CreateRecipe(recipeDTO);
        return Ok();
    }
}
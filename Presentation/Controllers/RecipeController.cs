using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipe = await _recipeService.GetAllRecipes();
        if (recipe == null)
            NoContent();
        return Ok(recipe);
    }
    [HttpGet]
    public async Task<IActionResult> GetById(int id)
    {
        var recipe = await _recipeService.GetById(id);
        if (recipe == null)
            NoContent();
        return Ok(recipe);
    }
    [HttpGet]
    public async Task<IActionResult> GetRecipeByIngredient(IngredientDTO ingredient)
    {
        var recipes = await _recipeService.GetRecipesWithIngredientsAsync(ingredient);
        if (recipes == null)
            NoContent();
        return Ok(recipes);
    }
    [HttpPost]
    public async Task<IActionResult> AddRecipe(RecipeDTO recipe)
    {
        var newRecipe = await _recipeService.CreateRecipe(recipe);
        return Ok(newRecipe);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(RecipeDTO recipe)
    {
        var deletedRecipe = await _recipeService.DeleteById(recipe);
        if (deletedRecipe == null)
            return NotFound();
        return Ok(deletedRecipe);
    }
}
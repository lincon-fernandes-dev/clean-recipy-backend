using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDTO>> GetAllRecipes();
        Task<RecipeDTO?> GetById(int id);
        Task<RecipeDTO> CreateRecipe(CreateRecipeDTO recipeDTO);
        Task<IEnumerable<RecipeDTO>> GetRecipesWithIngredientsAsync(IngredientDTO ingredientDTO);
        Task<RecipeDTO> UpdateRecipe(RecipeDTO recipeDTO);
        Task<RecipeDTO> DeleteById(int id);
    }
}

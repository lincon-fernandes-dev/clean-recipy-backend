using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDTO?>> GetAllRecipes();
        Task<RecipeDTO?> GetById(int id);
        Task<IEnumerable<RecipeDTO?>> GetRecipesWithIngredientsAsync(IngredientDTO ingredientDTO);
        Task<RecipeDTO?> CreateRecipe(RecipeDTO recipeDTO);
        Task<RecipeDTO?> UpdateRecipe(RecipeDTO recipeDTO);
        Task<RecipeDTO?> DeleteById(RecipeDTO dto);
    }
}

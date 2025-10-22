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
        Task<RecipeDTO?> GetAllRecipes();
        Task<RecipeDTO?> GetById(int id);
        Task<RecipeDTO?> DeleteById(int id);
        Task<RecipeDTO?> GetRecipeByIngredient(IngredientDTO ingredientDTO);
    }
}

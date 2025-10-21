using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecipeIngredientRepository : IBaseRepository<RecipeIngredient>
    {
        Task<IEnumerable<RecipeIngredient>> GetIngredientsByRecipeIdAsync(int recipeId);
    }
}
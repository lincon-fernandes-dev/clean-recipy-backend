using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecipeRepository : IBaseRepository<Recipe>
    {
        Task<IEnumerable<Recipe>?> GetRecipesAsync();
        Task<IEnumerable<Recipe>?> GetRecipesByUserIdAsync(int userId);
        Task<IEnumerable<Recipe>?> GetRecipesWithIngredientsAsync(Ingredient ingredient);
    }
}
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>?> GetIngredientsAsync();
        Task<IEnumerable<Ingredient>?> GetIngredientsByNameAsync(string name);
    }
}
using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<IngredientDTO?> GetByIdAsync(int id);
        Task<IEnumerable<IngredientDTO>> GetByNameAsync(string name);
        Task<IEnumerable<IngredientDTO>> GetAllAsync();
        Task<IngredientDTO> CreateAsync(IngredientDTO ingredientDTO);
        Task<IngredientDTO> UpdateAsync(IngredientDTO ingredientDTO);
        Task<bool> DeleteAsync(int id);
    }
}
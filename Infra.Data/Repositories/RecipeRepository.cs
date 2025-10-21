using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private ApplicationDbContext _context;
        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Recipe> CreateAsync(Recipe entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Recipe> UpdateAsync(Recipe entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Recipe> DeleteAsync(Recipe entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }
        public async Task<IEnumerable<Recipe>?> GetRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<IEnumerable<Recipe>?> GetRecipesByUserIdAsync(int userId)
        {
            return await _context.Recipes
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>?> GetRecipesWithIngredientsAsync(Ingredient ingredient)
        {
            return await _context.Recipes
                .Where(r => r.Ingredients.Any(ri => ri.IngredientId == ingredient.Id))
                .Include(r => r.User)
                .Include(r => r.Ingredients)
                    .ThenInclude(ri => ri.Ingredient)
                .OrderByDescending(r => r.CreatedDate)
                .ToListAsync();
        }
    }
}
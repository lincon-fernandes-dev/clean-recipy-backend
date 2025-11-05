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
        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if(ingredient.Id < 1)
                {
                    
                }
            };
            _context.Add(recipe);

            await _context.SaveChangesAsync();
            return recipe;
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
                .Where(i => i.IdUser == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>?> GetRecipesWithIngredientsAsync(Ingredient ingredient)
        {
            return await _context.Recipes
                .Where(r => r.Ingredients.Any(ri => ri.Id == ingredient.Id))
                .ToListAsync();
        }
    }
}
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
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(recipe.Id) ?? recipe; ;
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
            return await _context.Recipes
                .Include(r => r.User)              // Autor da receita
                .Include(r => r.Ingredients)       // Ingredientes
                .Include(r => r.Instructions)      // Modo de preparo
                .Include(r => r.RecipeTags)        // Tags
                    .ThenInclude(rt => rt.Tag)
                .Include(r => r.NutritionInfo)     // Informação nutricional
                .Include(r => r.RecipeLikes)       // Likes
                .Include(r => r.Comments)          // Comentários
                    .ThenInclude(c => c.User)      // Autor dos comentários
                .Include(r => r.Comments)          // Replies dos comentários
                    .ThenInclude(c => c.Replies)
                        .ThenInclude(r => r.User)
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.User)              // Autor da receita
                .Include(r => r.Ingredients)       // Ingredientes
                .Include(r => r.Instructions)      // Modo de preparo
                .Include(r => r.RecipeTags)        // Tags
                    .ThenInclude(rt => rt.Tag)
                .Include(r => r.NutritionInfo)     // Informação nutricional
                .Include(r => r.RecipeLikes)       // Likes
                .Include(r => r.Comments)          // Comentários
                    .ThenInclude(c => c.User)      // Autor dos comentários
                .AsSplitQuery() // Para melhor performance com muitos includes
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
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
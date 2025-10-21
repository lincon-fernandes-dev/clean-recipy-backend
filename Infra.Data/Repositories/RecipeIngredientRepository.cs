using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        ApplicationDbContext _context;
        public RecipeIngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RecipeIngredient> CreateAsync(RecipeIngredient entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipeIngredient> UpdateAsync(RecipeIngredient entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipeIngredient> DeleteAsync(RecipeIngredient entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipeIngredient?> GetByIdAsync(int id)
        {
            return await _context.RecipeIngredients.FindAsync(id);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetIngredientsByRecipeIdAsync(int recipeId)
        {
            return await _context.RecipeIngredients
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync();
        }
    }
}
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        ApplicationDbContext _context;
        public IngredientRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Ingredient> CreateAsync(Ingredient entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Ingredient> UpdateAsync(Ingredient entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Ingredient> DeleteAsync(Ingredient entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByNameAsync(string name)
        {
            return await _context.Ingredients
                    .Where(i => i.Name == name)
                    .ToListAsync();
        }
        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients.OrderBy(i => i.Name).ToListAsync();
        }
    }
}
using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public class RecipeTagRepository : IRecipeTagRepository
    {
        ApplicationDbContext _context;
        public RecipeTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RecipeTag> CreateAsync(RecipeTag entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipeTag> DeleteAsync(RecipeTag entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipeTag?> GetByIdAsync(int id)
        {
            return await _context.RecipeTags.FindAsync(id);
        }

        public async Task<RecipeTag> UpdateAsync(RecipeTag entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<RecipeTag>> GetAllAsync()
        {
            return await _context.RecipeTags.ToListAsync();
        }
    }
}

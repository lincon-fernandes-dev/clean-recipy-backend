using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
                .Where(t => t.Title == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> CreateAsync(Tag entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Tag> UpdateAsync(Tag entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Tag> DeleteAsync(Tag entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
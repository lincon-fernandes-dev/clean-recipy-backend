using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class InstructionRepository : IInstructionRepository
    {
        ApplicationDbContext _context;
        public InstructionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Instruction> CreateAsync(Instruction entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Instruction> DeleteAsync(Instruction entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Instruction?> GetByIdAsync(int id)
        {
            return await _context.Instructions.FindAsync(id);
        }

        public async Task<Instruction> UpdateAsync(Instruction entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<Instruction>> GetByIdRecipeAsync(int id)
        {
            return await _context.Instructions
                .Where(i => i.IdRecipe == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<Instruction>> GetAllAsync()
        {
            return await _context.Instructions.ToListAsync();
        }
    }
}
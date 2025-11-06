using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public class NutritionInfoRepository : INutritionInfoRepository
    {
        ApplicationDbContext _context;
        public NutritionInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<NutritionInfo> CreateAsync(NutritionInfo entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NutritionInfo> DeleteAsync(NutritionInfo entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NutritionInfo?> GetByIdAsync(int id)
        {
            return await _context.NutritionInfos.FindAsync(id);
        }

        public async Task<NutritionInfo> UpdateAsync(NutritionInfo entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<NutritionInfo>> GetAllAsync()
        {
            return await _context.NutritionInfos.ToListAsync();
        }
    }
}

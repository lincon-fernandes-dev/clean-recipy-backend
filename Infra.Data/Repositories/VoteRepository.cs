using Domain.Entities;
using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        ApplicationDbContext _context;
        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vote> CreateAsync(Vote entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vote> UpdateAsync(Vote entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vote> DeleteAsync(Vote entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Vote?> GetByIdAsync(int id)
        {
            return await _context.Votes.FindAsync(id);
        }

        public async Task<IEnumerable<Vote>?> GetVotesByRecipeIdAsync(int recipeId)
        {
            return await _context.Votes
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vote>?> GetVotesByUserIdAsync(int userId)
        {
            return await _context.Votes
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<int> GetVoteCountByRecipeAsync(int recipeId, bool? isUpvote = null)
        {
            var query = _context.Votes.Where(v => v.RecipeId == recipeId);

            if (isUpvote.HasValue)
            {
                query = query.Where(v => v.IsUpvote == isUpvote.Value);
            }

            return await query.CountAsync();
        }
    }
}
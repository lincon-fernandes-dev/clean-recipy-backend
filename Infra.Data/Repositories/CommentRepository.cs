using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        { 
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comment> DeleteAsync(Comment entity)
        {
            _context.Comments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync(Comment entity)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Comment>> GetCommentsByIdRecipeAsync(int idRecipe)
        {
            return await _context.Comments.Where(c => c.IdRecipe == idRecipe && c.ParentCommentId == null).ToListAsync();
        }
    }
}

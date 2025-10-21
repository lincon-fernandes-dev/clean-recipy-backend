using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVoteRepository : IBaseRepository<Vote>
    {
        Task<IEnumerable<Vote>?> GetVotesByRecipeIdAsync(int recipeId);
        Task<IEnumerable<Vote>?> GetVotesByUserIdAsync(int userId);
        Task<int> GetVoteCountByRecipeAsync(int recipeId, bool? isUpvote = null);
    }
}
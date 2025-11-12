using Domain.Entities;

namespace Domain.Interfaces;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<IEnumerable<Comment>> GetCommentsByIdRecipeAsync(int idRecipe);
}

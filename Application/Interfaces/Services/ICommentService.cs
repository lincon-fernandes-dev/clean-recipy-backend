using Application.DTOs;

namespace Application.Interfaces.Services;

public interface ICommentService
{
    Task<CommentDTO?> LikeCommentAsync(CommentDTO commentDTO);
    Task<CommentDTO> CreateCommentAsync(CreateCommentDTO commentDTO);
    Task<IEnumerable<CommentDTO>> GetRecipeCommentsAsync(int idRecipe);
    Task<CommentDTO> DeleteCommentAsync(CommentDTO commentDTO);
}

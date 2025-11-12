namespace Application.DTOs;

public class CommentDTO
{
    public int IdComment { get; set; } // 🔥 CHAVE PRIMÁRIA DO COMENTÁRIO
    public int IdUser { get; set; }
    public int IdRecipe { get; set; }
    public string Content { get; set; } = string.Empty;
    public int? ParentCommentId { get; set; }
    public CommentDTO? ParentComment { get; set; }
    public required UserDTO Author { get; set; }
    public bool isCommentLiked { get; set; }
    public List<CommentDTO> Replies { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

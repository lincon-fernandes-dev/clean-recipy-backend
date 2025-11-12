namespace Application.DTOs;

public class CreateCommentDTO
{
    public int IdUser { get; set; }
    public int IdRecipe { get; set; }
    public string Content { get; set; } = string.Empty;
    public int? ParentCommentId { get; set; }
}

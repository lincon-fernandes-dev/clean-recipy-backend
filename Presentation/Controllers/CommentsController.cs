using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> CreateComment(CreateCommentDTO commentDTO)
    {
        var comment = await _commentService.CreateCommentAsync(commentDTO);
        return Ok(comment);
    }
    [HttpPut]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> LikeComment(CommentDTO commentDTO)
    {
        var comment = await _commentService.LikeCommentAsync(commentDTO);
        return Ok(comment);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetRecipeComments(int idRecipe)
    {
        var recipes = await _commentService.GetRecipeCommentsAsync(idRecipe);
        return Ok(recipes);
    }
    [HttpDelete]
    public async Task<ActionResult<CommentDTO>> DeleteComment(CommentDTO commentDTO)
    {
        var recipes = await _commentService.DeleteCommentAsync(commentDTO);
        return Ok(recipes);
    }
}

using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _repository;
    public CommentService(ICommentRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CommentDTO> DeleteCommentAsync(CommentDTO commentDTO)
    {
        var entity = _mapper.Map<Comment>(commentDTO);
        return _mapper.Map<CommentDTO>(await _repository.DeleteAsync(entity));
    }

    public async Task<CommentDTO> CreateCommentAsync(CreateCommentDTO commentDTO)
    {
        var entity = _mapper.Map<Comment>(commentDTO);
        return _mapper.Map<CommentDTO>(await _repository.CreateAsync(entity));
    }

    public Task<IEnumerable<CommentDTO>> GetRecipeCommentsAsync(int idRecipe)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO?> LikeCommentAsync(CommentDTO commentDTO)
    {
        throw new NotImplementedException();
    }
}

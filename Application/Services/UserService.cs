using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Interfaces;
namespace Application.Services;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;

    }
    public async Task<UserDTO?> GetById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        var dto = _mapper.Map<UserDTO>(entity);
        return dto;
    }
    public async Task<UserDTO?> DeleteById(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
        {
            return null;
        }
        var deletedUser = _repository.DeleteAsync(entity);
        var dto = _mapper.Map<UserDTO>(deletedUser);
        return dto;
    }
}

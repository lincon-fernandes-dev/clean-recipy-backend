using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Validation;

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
    public async Task<UserDTO?> Create(UserDTO dto)
    {
        try
        {
            var status = UserStatus.Active;

            var user = new User(
                name: dto.Name,
                email: dto.Email,
                passwordHash: dto.PasswordHash,
                avatar: dto.Avatar,
                isVerified: dto.IsVerified,
                status: status,
                createdAt: DateTime.UtcNow,
                updatedAt: DateTime.UtcNow,
                createdBy: "system",
                lastModifiedBy: "system"
            );

            var retorno = await _repository.CreateAsync(user);
            return _mapper.Map<UserDTO>(retorno);
        }
        catch (DomainExceptionValidation ex)
        {
            Console.WriteLine($"Erro de validação: {ex.Message}");
            throw;
        }
    }
    public async Task<UserDTO?> Update(UserDTO dto)
    {
        var entity = _mapper.Map<User>(dto);
        return _mapper.Map<UserDTO>(await _repository.UpdateAsync(entity));
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
    public async Task<UserDTO?> GetByEmailAsync(string email)
    {
        var entity = await _repository.GetByEmailAsync(email);
        if (entity == null)
        {
            return null;
        }
        var dto = _mapper.Map<UserDTO>(entity);
        return dto;
    }
}

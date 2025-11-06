using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO?> Create(UserDTO dto);
        Task<UserDTO?> GetById(int id);
        Task<UserDTO?> Update(UserDTO dto);
        Task<UserDTO?> DeleteById(int id);
        Task<UserDTO?> GetByEmailAsync(string email);
    }
}
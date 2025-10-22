using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDTO?> GetById(int id);
        Task<UserDTO?> DeleteById(int id);
    }
}

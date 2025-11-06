using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface ITagService
    {
        Task<TagDTO> GetById(int id);
        Task<TagDTO> GetByName(string name);
        Task<IEnumerable<TagDTO>> GetAll();
        Task<TagDTO> Create(TagDTO tagDTO);
        Task<TagDTO> Update(TagDTO tagDTO);
        Task<bool> Delete(int id);
    }
}
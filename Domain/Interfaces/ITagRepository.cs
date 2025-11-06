using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<Tag?> GetByNameAsync(string name);
    }
}

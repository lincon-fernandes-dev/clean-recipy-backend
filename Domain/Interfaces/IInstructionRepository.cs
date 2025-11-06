using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IInstructionRepository : IBaseRepository<Instruction>
    {
        Task<IEnumerable<Instruction>> GetByIdRecipeAsync(int id);
    }
}

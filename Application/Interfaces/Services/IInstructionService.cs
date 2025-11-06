using Application.DTOs;

namespace Application.Interfaces.Services
{
    public interface IInstructionService
    {
        Task<InstructionDTO> GetById(int id);
        Task<IEnumerable<InstructionDTO>> GetByRecipeId(int recipeId);
        Task<InstructionDTO> Create(InstructionDTO instructionDTO);
        Task<InstructionDTO> Update(InstructionDTO instructionDTO);
        Task<bool> Delete(int id);
    }
}
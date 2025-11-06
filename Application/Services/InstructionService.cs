using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class InstructionService : IInstructionService
    {
        private readonly IMapper _mapper;
        private readonly IInstructionRepository _instructionRepository;

        public InstructionService(IMapper mapper, IInstructionRepository instructionRepository)
        {
            _mapper = mapper;
            _instructionRepository = instructionRepository;
        }

        public async Task<InstructionDTO> GetById(int id)
        {
            var instruction = await _instructionRepository.GetByIdAsync(id);
            return _mapper.Map<InstructionDTO>(instruction);
        }

        public async Task<IEnumerable<InstructionDTO>> GetByRecipeId(int recipeId)
        {
            // Você pode adicionar um método específico no repository se necessário
            var recipeInstructions = await _instructionRepository.GetByIdRecipeAsync(recipeId);
            
            return _mapper.Map<IEnumerable<InstructionDTO>>(recipeInstructions);
        }

        public async Task<InstructionDTO> Create(InstructionDTO instructionDTO)
        {
            var instruction = _mapper.Map<Instruction>(instructionDTO);
            var created = await _instructionRepository.CreateAsync(instruction);
            return _mapper.Map<InstructionDTO>(created);
        }

        public async Task<InstructionDTO> Update(InstructionDTO instructionDTO)
        {
            var instruction = _mapper.Map<Instruction>(instructionDTO);
            var updated = await _instructionRepository.UpdateAsync(instruction);
            return _mapper.Map<InstructionDTO>(updated);
        }

        public async Task<bool> Delete(int id)
        {
            var instruction = await _instructionRepository.GetByIdAsync(id);
            if (instruction == null) return false;

            await _instructionRepository.DeleteAsync(instruction);
            return true;
        }
    }
}
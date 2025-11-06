using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;
    public class IngredientService : IIngredientService
{
    private readonly IMapper _mapper;
    private readonly IIngredientRepository _repository;
    public IngredientService(IIngredientRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IngredientDTO> CreateAsync(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.CreateAsync(entity));
    }
    public async Task<IngredientDTO> UpdateAsync(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.UpdateAsync(entity));
    }
    public async Task<IngredientDTO?> Delete(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.DeleteAsync(entity));
    }

    public async Task<IEnumerable<IngredientDTO>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<IngredientDTO>>(await _repository.GetAllAsync());
    }

    public async Task<IEnumerable<IngredientDTO>> GetByNameAsync(string name)
    {
        return _mapper.Map<IEnumerable<IngredientDTO>>(await _repository.GetIngredientsByNameAsync(name));
    }

    public async Task<IngredientDTO?> GetByIdAsync(int id)
    {
        return _mapper.Map<IngredientDTO?>(await _repository.GetByIdAsync(id));
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
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

    public async Task<IngredientDTO?> Create(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.CreateAsync(entity));
    }
    public async Task<IngredientDTO> Update(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.UpdateAsync(entity));
    }
    public async Task<IngredientDTO?> Delete(IngredientDTO ingredient)
    {
        var entity = _mapper.Map<Ingredient>(ingredient);
        return _mapper.Map<IngredientDTO>(await _repository.DeleteAsync(entity));
    }

    public async Task<IEnumerable<IngredientDTO>?> GetAll()
    {
        return _mapper.Map<IEnumerable<IngredientDTO>?>(await _repository.GetIngredientsAsync());
    }

    public async Task<IEnumerable<IngredientDTO>?> GetByName(string name)
    {
        return _mapper.Map<IEnumerable<IngredientDTO>?>(await _repository.GetIngredientsByNameAsync(name));
    }
}
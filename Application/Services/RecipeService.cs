using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RecipeService : IRecipeService
    {
    private readonly IMapper _mapper;
    private readonly IRecipeRepository _recipeRpository;
        public RecipeService(IMapper mapper, IRecipeRepository repository)
        {
            _recipeRpository = repository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<RecipeDTO?>> GetAllRecipes()
        {
            var recipeEntities = await _recipeRpository.GetRecipesAsync();
            var dtos = _mapper.Map<IEnumerable<RecipeDTO?>>(recipeEntities);
            return dtos;
        }
        public async Task<RecipeDTO?> GetById(int id)
        {
            var recipeEntity = await _recipeRpository.GetByIdAsync(id);
            var dto = _mapper.Map<RecipeDTO?>(recipeEntity);
            return dto;
        }

        public async Task<IEnumerable<RecipeDTO?>> GetRecipesWithIngredientsAsync(IngredientDTO ingredientDTO)
        {
            var entity = _mapper.Map<Ingredient>(ingredientDTO);
            var recipeEntity = await _recipeRpository.GetRecipesWithIngredientsAsync(entity);
            var dto = _mapper.Map<IEnumerable<RecipeDTO?>>(recipeEntity);
            return dto;
        }
        public async Task<RecipeDTO?> CreateRecipe(RecipeDTO recipeDTO)
        {
            var recipeEntity = _mapper.Map<Recipe>(recipeDTO);
            var recipe = await _recipeRpository.CreateAsync(recipeEntity);
            var recipeDto = _mapper.Map<RecipeDTO?>(recipe);
            return recipeDto;
        }
        public async Task<RecipeDTO?> UpdateRecipe(RecipeDTO dto)
        {
            var entity = _mapper.Map<Recipe>(dto);
            return _mapper.Map<RecipeDTO>(await _recipeRpository.UpdateAsync(entity));   
        }
        public async Task<RecipeDTO?> DeleteById(RecipeDTO recipe)
        {
            var recipeEntity = _mapper.Map<Recipe>(recipe);
            return _mapper.Map<RecipeDTO>(await _recipeRpository.DeleteAsync(recipeEntity));
        }
    }
}

using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IInstructionRepository _instructionRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRecipeTagRepository _recipeTagRepository;
        private readonly INutritionInfoRepository _nutritionInfoRepository;

        public RecipeService(
            IMapper mapper,
            IRecipeRepository recipeRepository,
            IUserRepository userRepository,
            IIngredientRepository ingredientRepository,
            IInstructionRepository instructionRepository,
            ITagRepository tagRepository,
            IRecipeTagRepository recipeTagRepository,
            INutritionInfoRepository nutritionInfoRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _ingredientRepository = ingredientRepository;
            _instructionRepository = instructionRepository;
            _tagRepository = tagRepository;
            _recipeTagRepository = recipeTagRepository;
            _nutritionInfoRepository = nutritionInfoRepository;
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllRecipes()
        {
            var recipeEntities = await _recipeRepository.GetAllAsync();
            var recipesdto = new List<RecipeDTO>();
            foreach (var recipe in recipeEntities)
            {
                recipesdto.Add(MapToRecipeDTO(recipe));
            }
            return recipesdto;
        }

        public async Task<RecipeDTO?> GetById(int id)
        {
            var recipeEntity = await _recipeRepository.GetByIdAsync(id);
            return MapToRecipeDTO(recipeEntity);
        }

        public async Task<RecipeDTO> CreateRecipe(CreateRecipeDTO recipeDTO)
        {
            // Validar se o autor existe
            var author = await _userRepository.GetByIdAsync(recipeDTO.AuthorId);
            if (author == null)
                throw new ArgumentException($"User with id {recipeDTO.AuthorId} not found");

            // 🔥 CORREÇÃO: Criar a receita primeiro sem instruções
            var recipeEntity = new Recipe(
                title: recipeDTO.Title,
                description: recipeDTO.Description,
                userId: recipeDTO.AuthorId,
                imageUrl: recipeDTO.ImageUrl,
                preparationTime: recipeDTO.PreparationTime,
                servings: recipeDTO.Servings,
                difficulty: recipeDTO.Difficulty,
                createdAt: DateTime.UtcNow,
                updatedAt: DateTime.UtcNow,
                createdBy: author.Name,
                lastModifiedBy: author.Name
            );

            // Salvar a receita para obter o ID
            var createdRecipe = await _recipeRepository.CreateAsync(recipeEntity);

            // 🔥 AGORA podemos criar as instruções com o ID da receita
            var instructions = new List<Instruction>();
            for (int i = 0; i < recipeDTO.Instructions.Count; i++)
            {
                var instructionDto = recipeDTO.Instructions[i];
                var instruction = new Instruction(
                    idRecipe: createdRecipe.Id,
                    content: instructionDto.Content,
                    stepNumber: i + 1, // Step numbers começam em 1
                    createdAt: DateTime.UtcNow,
                    updatedAt: DateTime.UtcNow,
                    createdBy: author.Name,
                    lastModifiedBy: author.Name
                );
                var createdInstruction = await _instructionRepository.CreateAsync(instruction);
                instructions.Add(createdInstruction);
            }

            // 🔥 Atualizar a receita com as instruções criadas
            createdRecipe.UpdateInstructions(instructions, author.Name);

            // Adicionar ingredientes
            foreach (var ingredientDto in recipeDTO.Ingredients)
            {
                var ingredient = new Ingredient(
                    name: ingredientDto.Name,
                    idRecipe: createdRecipe.Id,
                    createdAt: DateTime.UtcNow,
                    updatedAt: DateTime.UtcNow,
                    createdBy: author.Name,
                    lastModifiedBy: author.Name
                );
                await _ingredientRepository.CreateAsync(ingredient);
            }

            // Adicionar tags (se houver)
            if (recipeDTO.Tags != null && recipeDTO.Tags.Any())
            {
                foreach (var tagName in recipeDTO.Tags)
                {
                    var tag = await _tagRepository.GetByNameAsync(tagName);

                    if (tag == null)
                    {
                        tag = new Tag(
                            title: tagName,
                            createdAt: DateTime.UtcNow,
                            updatedAt: DateTime.UtcNow,
                            createdBy: author.Name,
                            lastModifiedBy: author.Name
                        );
                        tag = await _tagRepository.CreateAsync(tag);
                    }

                    var recipeTag = new RecipeTag(
                        idTag: tag.Id,
                        idRecipe: createdRecipe.Id,
                        createdAt: DateTime.UtcNow,
                        updatedAt: DateTime.UtcNow,
                        createdBy: author.Name,
                        lastModifiedBy: author.Name
                    );
                    await _recipeTagRepository.CreateAsync(recipeTag);
                }
            }

            // Adicionar informação nutricional (se houver)
            if (recipeDTO.NutritionInfo != null)
            {
                var nutritionInfo = new NutritionInfo(
                    idRecipe: createdRecipe.Id,
                    calories: recipeDTO.NutritionInfo.Calories,
                    proteins: recipeDTO.NutritionInfo.Proteins,
                    carbs: recipeDTO.NutritionInfo.Carbs,
                    fat: recipeDTO.NutritionInfo.Fat,
                    createdAt: DateTime.UtcNow,
                    updatedAt: DateTime.UtcNow,
                    createdBy: author.Name,
                    lastModifiedBy: author.Name
                );

                await _nutritionInfoRepository.CreateAsync(nutritionInfo);
            }

            // 🔥 Recarregar a receita com todos os relacionamentos
            var completeRecipe = await _recipeRepository.GetByIdAsync(createdRecipe.Id);
            return MapToRecipeDTO(completeRecipe);
        }

        public async Task<IEnumerable<RecipeDTO>> GetRecipesWithIngredientsAsync(IngredientDTO ingredientDTO)
        {
            var recipeEntities = await _recipeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeDTO>>(recipeEntities);
        }

        public async Task<RecipeDTO> UpdateRecipe(RecipeDTO dto)
        {
            var entity = _mapper.Map<Recipe>(dto);
            var updatedRecipe = await _recipeRepository.UpdateAsync(entity);
            return _mapper.Map<RecipeDTO>(updatedRecipe);
        }

        public async Task<RecipeDTO> DeleteById(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe == null)
                throw new ArgumentException($"Recipe with id {id} not found");

            var deletedRecipe = await _recipeRepository.DeleteAsync(recipe);
            return _mapper.Map<RecipeDTO>(deletedRecipe);
        }

        private RecipeDTO MapToRecipeDTO(Recipe recipe)
        {
            if (recipe == null) return new RecipeDTO();

            return new RecipeDTO
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                PreparationTime = recipe.PreparationTime,
                Servings = recipe.Servings,
                Difficulty = recipe.Difficulty,
                Author = recipe.User != null ? new UserDTO
                {
                    Id = recipe.User.Id,
                    Name = recipe.User.Name,
                    Email = recipe.User.Email,
                    Avatar = recipe.User.Avatar,
                    IsVerified = recipe.User.IsVerified
                } : null,
                Ingredients = recipe.Ingredients?.Select(i => new IngredientDTO
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList() ?? new List<IngredientDTO>(),
                Instructions = recipe.Instructions?.Select(i => new InstructionDTO
                {
                    Id = i.Id,
                    Content = i.Content,
                    StepNumber = i.StepNumber
                }).OrderBy(i => i.StepNumber).ToList() ?? new List<InstructionDTO>(),
                Tags = recipe.RecipeTags?.Select(rt => rt.Tag?.Title ?? string.Empty)
                                    .Where(title => !string.IsNullOrEmpty(title))
                                    .ToList() ?? new List<string>(),
                NutritionInfo = recipe.NutritionInfo != null ? new NutritionInfoDTO
                {
                    Id = recipe.NutritionInfo.Id,
                    Calories = recipe.NutritionInfo.Calories,
                    Proteins = recipe.NutritionInfo.Proteins,
                    Carbs = recipe.NutritionInfo.Carbs,
                    Fat = recipe.NutritionInfo.Fat
                } : null,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt
            };
        }
    }
}
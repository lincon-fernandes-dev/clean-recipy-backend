using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            // 🔥 CORREÇÃO: Mapeamento específico para Recipe -> RecipeDTO
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.RecipeTags != null ? src.RecipeTags.Select(rt => rt.Tag.Title) : new List<string>()))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => src.Instructions))
                .ForMember(dest => dest.NutritionInfo, opt => opt.MapFrom(src => src.NutritionInfo));

            // Mapeamentos simples
            CreateMap<User, UserDTO>();
            CreateMap<Ingredient, IngredientDTO>();
            CreateMap<Instruction, InstructionDTO>();
            CreateMap<NutritionInfo, NutritionInfoDTO>();
            CreateMap<Tag, TagDTO>();

            // Mapeamento reverso (se necessário)
            CreateMap<RecipeDTO, Recipe>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore())
                .ForMember(dest => dest.Instructions, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore())
                .ForMember(dest => dest.NutritionInfo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeLikes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            // Mapeamento para criação
            CreateMap<CreateRecipeDTO, Recipe>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Ingredients, opt => opt.Ignore())
                .ForMember(dest => dest.Instructions, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore())
                .ForMember(dest => dest.NutritionInfo, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeLikes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            // 🔥 REMOVER este mapeamento problemático
            // CreateMap<Tag, string>().ConvertUsing(t => t.Title);
        }
    }
}
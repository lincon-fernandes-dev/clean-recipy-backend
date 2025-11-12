namespace Application.DTOs
{
    public class RecipeDTO
    {
        public int IdRecipe { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int Servings { get; set; }
        public string Difficulty { get; set; } = string.Empty;

        // Relacionamentos
        public UserDTO? Author { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = [];
        public List<InstructionDTO> Instructions { get; set; } = [];
        public List<CommentDTO> Comments { get; set; } = [];
        public List<string> Tags { get; set; } = [];
        public NutritionInfoDTO? NutritionInfo { get; set; }

        public int Likes { get; set; }
        public double Rating { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
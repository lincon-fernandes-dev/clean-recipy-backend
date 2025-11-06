namespace Application.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int Servings { get; set; }
        public string Difficulty { get; set; } = string.Empty;

        // Relacionamentos
        public UserDTO? Author { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = new();
        public List<InstructionDTO> Instructions { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public NutritionInfoDTO? NutritionInfo { get; set; }

        // 🔥 CORREÇÃO: Estas propriedades devem ser int, não coleções
        public int Likes { get; set; }
        public int Comments { get; set; }
        public double Rating { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
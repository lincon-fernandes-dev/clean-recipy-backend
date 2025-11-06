namespace Application.DTOs
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int Servings { get; set; }
        public string Difficulty { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public List<CreateIngredientDTO> Ingredients { get; set; } = new();
        public List<CreateInstructionDTO> Instructions { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public CreateNutritionInfoDTO? NutritionInfo { get; set; }
    }

    public class CreateIngredientDTO
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateInstructionDTO
    {
        public string Content { get; set; } = string.Empty;
        public int stepNumber { get; set; }
    }

    public class CreateNutritionInfoDTO
    {
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
    }
}
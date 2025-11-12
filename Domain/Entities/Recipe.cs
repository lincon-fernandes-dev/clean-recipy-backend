using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Recipe : Entity
    {
        public int IdUser { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public IEnumerable<Instruction> Instructions { get; set; } = new List<Instruction>();
        public IEnumerable<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
        public IEnumerable<RecipeLike> RecipeLikes { get; set; } = new List<RecipeLike>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public NutritionInfo NutritionInfo { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public int PreparationTime { get; private set; }
        public int Servings { get; private set; }
        public string Difficulty { get; private set; } = string.Empty;

        // 🔹 Relacionamentos
        public User? User { get; private set; }

        private Recipe() { }

        public Recipe(string title, string description, int userId, string imageUrl, int preparationTime, int servings, string difficulty, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(title, description, userId, imageUrl, preparationTime, servings, difficulty);

            Title = title;
            Description = description;
            IdUser = userId;
            ImageUrl = imageUrl;
            PreparationTime = preparationTime;
            Servings = servings;
            Difficulty = difficulty;
        }

        public Recipe(int id, string title, string description, int userId, string imageUrl, int preparationTime, int servings, string difficulty, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(title, description, userId, imageUrl, preparationTime, servings, difficulty);

            Id = id;
            Title = title;
            Description = description;
            IdUser = userId;
            ImageUrl = imageUrl;
            PreparationTime = preparationTime;
            Servings = servings;
            Difficulty = difficulty;
        }

        public void Update(string title, string description, IEnumerable<Instruction> instructions, string imageUrl, int preparationTime, int servings, string difficulty, string modifiedBy)
        {
            Validate(title, description, IdUser, imageUrl, preparationTime, servings, difficulty);
            ValidateInstructions(instructions);
            ValidateModifiedBy(modifiedBy);

            Title = title;
            Description = description;
            Instructions = instructions;
            ImageUrl = imageUrl;
            PreparationTime = preparationTime;
            Servings = servings;
            Difficulty = difficulty;

            MarkAsModified(modifiedBy);
        }

        public void UpdateTitle(string title, string modifiedBy)
        {
            ValidateTitle(title);
            ValidateModifiedBy(modifiedBy);

            Title = title;
            MarkAsModified(modifiedBy);
        }

        public void UpdateDescription(string description, string modifiedBy)
        {
            ValidateDescription(description);
            ValidateModifiedBy(modifiedBy);

            Description = description;
            MarkAsModified(modifiedBy);
        }

        public void UpdateInstructions(IEnumerable<Instruction> instructions, string modifiedBy)
        {
            ValidateInstructions(instructions);
            ValidateModifiedBy(modifiedBy);

            Instructions = instructions;
            MarkAsModified(modifiedBy);
        }

        public void UpdateImageUrl(string imageUrl, string modifiedBy)
        {
            ValidateImageUrl(imageUrl);
            ValidateModifiedBy(modifiedBy);

            ImageUrl = imageUrl;
            MarkAsModified(modifiedBy);
        }

        public void UpdatePreparationTime(int preparationTime, string modifiedBy)
        {
            ValidatePreparationTime(preparationTime);
            ValidateModifiedBy(modifiedBy);

            PreparationTime = preparationTime;
            MarkAsModified(modifiedBy);
        }

        public void UpdateServings(int servings, string modifiedBy)
        {
            ValidateServings(servings);
            ValidateModifiedBy(modifiedBy);

            Servings = servings;
            MarkAsModified(modifiedBy);
        }

        public void UpdateDifficulty(string difficulty, string modifiedBy)
        {
            ValidateDifficulty(difficulty);
            ValidateModifiedBy(modifiedBy);

            Difficulty = difficulty;
            MarkAsModified(modifiedBy);
        }

        public void UpdateNutritionInfo(NutritionInfo nutritionInfo, string modifiedBy)
        {
            ValidateModifiedBy(modifiedBy);
            NutritionInfo = nutritionInfo;
            MarkAsModified(modifiedBy);
        }

        public void AddIngredient(Ingredient ingredient, string modifiedBy)
        {
            ValidateModifiedBy(modifiedBy);
            Ingredients.Add(ingredient);
            MarkAsModified(modifiedBy);
        }

        public void RemoveIngredient(Ingredient ingredient, string modifiedBy)
        {
            ValidateModifiedBy(modifiedBy);
            Ingredients.Remove(ingredient);
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string title, string description, int userId, string imageUrl, int preparationTime, int servings, string difficulty)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            ValidateUserId(userId);
            ValidateImageUrl(imageUrl);
            ValidatePreparationTime(preparationTime);
            ValidateServings(servings);
            ValidateDifficulty(difficulty);
        }

        private static void ValidateTitle(string title)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(title), "O nome da receita é obrigatório.");
            ValidateDomain(title.Trim().Length < 3, "O nome da receita deve ter pelo menos 3 caracteres.");
            ValidateDomain(title.Trim().Length > 100, "O nome da receita não pode ultrapassar 100 caracteres.");
        }

        private static void ValidateDescription(string description)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(description), "A descrição é obrigatória.");
            ValidateDomain(description.Trim().Length < 25, "A descrição deve ter pelo menos 25 caracteres.");
            ValidateDomain(description.Trim().Length > 524, "A descrição não pode ultrapassar 524 caracteres.");
        }

        private static void ValidateInstructions(IEnumerable<Instruction> instructions)
        {
            ValidateDomain(instructions == null, "As instruções são obrigatórias.");
            ValidateDomain(!instructions.Any(), "A receita deve ter pelo menos uma instrução.");
            ValidateDomain(instructions.Count() > 50, "A receita não pode ter mais de 50 instruções.");
        }

        private static void ValidateUserId(int userId)
        {
            ValidateDomain(userId < 1, "Id do usuário inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateImageUrl(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                return;

            ValidateDomain(imageUrl.Length > 500, "A URL da imagem não pode ultrapassar 500 caracteres.");

            if (imageUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                ValidateDomain(!Uri.TryCreate(imageUrl, UriKind.Absolute, out _), "A URL da imagem é inválida.");
            }
        }

        private static void ValidatePreparationTime(int preparationTime)
        {
            ValidateDomain(preparationTime < 1, "O tempo de preparo deve ser pelo menos 1 minuto.");
            ValidateDomain(preparationTime > 1440, "O tempo de preparo não pode exceder 1440 minutos (24 horas).");
        }

        private static void ValidateServings(int servings)
        {
            ValidateDomain(servings < 1, "A receita deve servir pelo menos 1 pessoa.");
            ValidateDomain(servings > 100, "A receita não pode servir mais de 100 pessoas.");
        }

        private static void ValidateDifficulty(string difficulty)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(difficulty), "A dificuldade é obrigatória.");

            var validDifficulties = new[] { "Fácil", "Médio", "Difícil" };
            ValidateDomain(!validDifficulties.Contains(difficulty.Trim()), "A dificuldade deve ser: Fácil, Médio ou Difícil.");
        }

        private static void ValidateModifiedBy(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "Para atualizar a receita é necessário fornecer o nome do usuário que está modificando.");
            ValidateDomain(modifiedBy.Trim().Length < 4, "O nome de usuário para modificação deve ter pelo menos 4 caracteres.");
        }

        public int GetLikesCount()
        {
            return RecipeLikes?.Count() ?? 0;
        }

        public int GetCommentsCount()
        {
            return Comments?.Count() ?? 0;
        }

        public bool HasTags()
        {
            return RecipeTags?.Any() ?? false;
        }

        public bool HasIngredients()
        {
            return Ingredients?.Any() ?? false;
        }
    }
}
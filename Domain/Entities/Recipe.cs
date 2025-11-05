using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Recipe : Entity
    {
        public int IdUser { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public IEnumerable<Instruction> Instructions { get; private set; }
        public IEnumerable<RecipeTag> RecipeTags { get; private set; }
        public IEnumerable<RecipeLike> RecipeLikes { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public NutritionInfo NutritionInfo { get; private set; }
        public string ImageUrl { get; private set; } = string.Empty;
        public int PreparationTime {  get; private set; }
        public int Servings { get; private set; }
        public string Difficulty { get; private set; } = string.Empty;

        // 🔹 Relacionamentos
        public User? User { get; private set; }

        public ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        private Recipe() { } // EF Core requer construtor privado

        public Recipe(string title, string description, IEnumerable<Instruction> instructions, int userId, string imageUrl, int preparationTime, int servings, string difficult)
        {
            Validate(title, description, instructions, userId, imageUrl, preparationTime, servings, difficult);

            Title = title;
            Description = description;
            Instructions = instructions;
            IdUser = userId;
        }

        public Recipe(int id, string title, string description, IEnumerable<Instruction> instructions, int userId, string imageUrl, int preparationTime, int servings, string difficult)
        {
            Validate(title, description, instructions, userId, imageUrl, preparationTime, servings, difficult);
            ValidateDomain(id < 1, "Id inválido, Id deve ser um numero inteiro e positivo");

            Id = id;
            Title = title;
            Description = description;
            Instructions = instructions;
            IdUser = userId;
        }

        public void Update(string name, string description, IEnumerable<Instruction> instructions, string modifiedBy)
        {
            Validate(name, description, instructions, this.IdUser, this.ImageUrl, this.PreparationTime, Servings, Difficulty);
            ValidateDomain(string.IsNullOrEmpty(modifiedBy), "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando");
            ValidateDomain(modifiedBy.Length < 4, "O nome de usuario para modificação deve ter pelo menos 4 caracteres");

            Title = name;
            Description = description;
            Instructions = instructions;

            MarkAsModified(modifiedBy);
        }

        private static void Validate(string title, string description, IEnumerable<Instruction> instructions, int userId, string imageUrl, int preparationTime, int servings, string difficult)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(title), "O nome da receita é obrigatório.");
            ValidateDomain(title.Length < 3, "O nome da receita deve ter pelo menos 3 caracteres.");
            ValidateDomain(title.Length > 100, "O nome da receita não pode ultrapassar 100 caracteres.");

            ValidateDomain(string.IsNullOrWhiteSpace(description), "A descrição é obrigatória.");
            ValidateDomain(description.Length > 524, "A descrição não pode ultrapassar 524 caracteres.");
            ValidateDomain(description.Length < 25, "A descrição deve ter pelo menos 25 caracteres.");

            ValidateDomain(userId < 1, "UserId inválido, Id deve ser um numero inteiro e positivo");

        }
    }
}

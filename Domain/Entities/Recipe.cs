using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Recipe : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Instructions { get; private set; } = string.Empty;

        // 🔹 Relacionamentos
        public int UserId { get; private set; }
        public User? User { get; private set; }

        public ICollection<RecipeIngredient> Ingredients { get; private set; } = new List<RecipeIngredient>();
        public ICollection<Vote> Votes { get; private set; } = new List<Vote>();

        private Recipe() { } // EF Core requer construtor privado

        public Recipe(string name, string description, string instructions, int userId, string createdBy)
        {
            Validate(name, description, instructions, userId, createdBy);

            Name = name;
            Description = description;
            Instructions = instructions;
            UserId = userId;
            CreatedBy = createdBy;
        }

        public Recipe(int id, string name, string description, string instructions, int userId, string createdBy)
        {
            Validate(name, description, instructions, userId, createdBy);
            ValidateDomain(id < 1, "Id inválido, Id deve ser um numero inteiro e positivo");

            Id = id;
            Name = name;
            Description = description;
            Instructions = instructions;
            UserId = userId;
            CreatedBy = createdBy;
        }

        public void Update(string name, string description, string instructions, string modifiedBy)
        {
            Validate(name, description, instructions, this.UserId, this.CreatedBy);
            ValidateDomain(string.IsNullOrEmpty(modifiedBy), "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando");
            ValidateDomain(modifiedBy.Length < 4, "O nome de usuario para modificação deve ter pelo menos 4 caracteres");

            Name = name;
            Description = description;
            Instructions = instructions;

            MarkAsModified(modifiedBy);
        }

        private static void Validate(string name, string description, string instructions, int userId, string createdBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome da receita é obrigatório.");
            ValidateDomain(name.Length < 3, "O nome da receita deve ter pelo menos 3 caracteres.");
            ValidateDomain(name.Length > 100, "O nome da receita não pode ultrapassar 100 caracteres.");

            ValidateDomain(string.IsNullOrWhiteSpace(description), "A descrição é obrigatória.");
            ValidateDomain(description.Length > 524, "A descrição não pode ultrapassar 524 caracteres.");
            ValidateDomain(description.Length < 25, "A descrição deve ter pelo menos 25 caracteres.");

            ValidateDomain(string.IsNullOrWhiteSpace(instructions), "As instruções são obrigatórias.");
            ValidateDomain(instructions.Length < 25, "As instruções devem ter pelo menos 25 caracteres.");
            ValidateDomain(instructions.Length > 600, "As instruções não podem ultrapassar 600 caracteres.");

            ValidateDomain(userId < 1, "UserId inválido, Id deve ser um numero inteiro e positivo");

            ValidateDomain(string.IsNullOrEmpty(createdBy), "O nome de usuario é obrigatorio");
            ValidateDomain(createdBy.Length < 4, "O nome de usuario deve ter pelo menos 4 caracteres");
        }
    }
}

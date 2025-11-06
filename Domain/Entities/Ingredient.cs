using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Ingredient : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public int IdRecipe { get; private set; }
        public Recipe Recipe { get; private set; }

        private Ingredient() { }

        public Ingredient(string name, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(name, idRecipe);

            Name = name.Trim();
            IdRecipe = idRecipe;
        }

        public Ingredient(int id, string name, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(name, idRecipe);

            Id = id;
            Name = name.Trim();
            IdRecipe = idRecipe;
        }

        public void UpdateName(string name, string modifiedBy)
        {
            ValidateName(name);
            ValidateModifiedBy(modifiedBy);

            Name = name;
            MarkAsModified(modifiedBy);
        }

        public void UpdateRecipe(int newIdRecipe, string modifiedBy)
        {
            ValidateIdRecipe(newIdRecipe);
            ValidateModifiedBy(modifiedBy);

            IdRecipe = newIdRecipe;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string name, int idRecipe)
        {
            ValidateName(name);
            ValidateIdRecipe(idRecipe);
        }

        private static void ValidateName(string name)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome do ingrediente é obrigatório.");
            ValidateDomain(name.Trim().Length < 2, "O nome do ingrediente deve ter pelo menos 2 caracteres.");
            ValidateDomain(name.Trim().Length > 200, "O nome do ingrediente não pode ultrapassar 200 caracteres.");
        }

        private static void ValidateIdRecipe(int idRecipe)
        {
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateModifiedBy(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "Para atualizar o ingrediente é necessário fornecer o nome do usuário que está modificando.");
            ValidateDomain(modifiedBy.Trim().Length < 3, "O nome de usuário para modificação deve ter pelo menos 3 caracteres.");
            ValidateDomain(modifiedBy.Trim().Length > 100, "O nome de usuário deve ter no máximo 100 caracteres.");
        }
    }
}
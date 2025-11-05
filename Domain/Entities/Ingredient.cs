using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Ingredient : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public int IdRecipe { get; private set; }
        public Recipe Recipe { get; private set; }

        private Ingredient() { }

        public Ingredient(string name, int idRecipe)
        {
            Validate(name, idRecipe);

            Name = name;
            IdRecipe = idRecipe;
        }

        public Ingredient(int id, string name, int idRecipe)
        {
            Validate(name, idRecipe);
            ValidateDomain(id < 1, "Id inválido, Id deve ser um numero inteiro e positivo");

            Id = id;
            Name = name;
            IdRecipe = idRecipe;
        }

        public void UpdateName(string name, string modifiedBy)
        {
            ValidateDomain(string.IsNullOrEmpty(modifiedBy), "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando");
            ValidateDomain(modifiedBy.Length < 4, "O nome de usuario para modificação deve ter pelo menos 4 caracteres");
            ValidateDomain(modifiedBy.Length > 128, "O nome de usuario deve ter no maximo 128 caracteres");

            Name = name;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string name, int idRecipe)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome do ingrediente é obrigatório.");
            ValidateDomain(name.Length < 2, "O nome do ingrediente deve ter pelo menos 2 caracteres.");
            ValidateDomain(name.Length > 100, "O nome do ingrediente não pode ultrapassar 100 caracteres.");

            
        }
    }
}

using Domain.Validation;

namespace Domain.Entities
{
    public sealed class Ingredient : Entity
    {
        public string Name { get; private set; } = string.Empty;

        private Ingredient() { }

        public Ingredient(string name, string createdBy)
        {
            Validate(name, createdBy);

            Name = name;
            CreatedBy = createdBy;
        }

        public Ingredient(int id, string name, string createdBy)
        {
            Validate(name, createdBy);
            ValidateDomain(id < 1, "Id inválido, Id deve ser um numero inteiro e positivo");

            Id = id;
            Name = name;
            CreatedBy = createdBy;
        }

        public void UpdateName(string name, string modifiedBy)
        {
            Validate(name, this.CreatedBy);
            ValidateDomain(string.IsNullOrEmpty(modifiedBy), "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando");
            ValidateDomain(modifiedBy.Length < 4, "O nome de usuario para modificação deve ter pelo menos 4 caracteres");

            Name = name;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string name, string createdBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome do ingrediente é obrigatório.");
            ValidateDomain(name.Length < 2, "O nome do ingrediente deve ter pelo menos 2 caracteres.");
            ValidateDomain(name.Length > 100, "O nome do ingrediente não pode ultrapassar 100 caracteres.");

            ValidateDomain(string.IsNullOrEmpty(createdBy), "O nome de usuario é obrigatorio");
            ValidateDomain(createdBy.Length < 4, "O nome de usuario deve ter pelo menos 4 caracteres");
        }
    }
}

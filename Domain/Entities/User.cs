using Domain.Enums;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserStatus Status { get; private set; }
        public ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();
        public ICollection<Vote> Votes { get; private set; } = new List<Vote>();

        private User() { } // EF Core requer construtor privado

        public User(string name, string email, string passwordHash, UserStatus status, string createdBy)
        {
            Validate(name, email, passwordHash);
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedBy = createdBy;
            Status = status;
        }
        public User(int id, string name, string email, string passwordHash, UserStatus status, string createdBy)
        {
            Validate(name, email, passwordHash);
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");

            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedBy = createdBy;
            Status = status;
        }

        public void UpdateProfile(string name, string email, string modifiedBy)
        {
            Validate(name, email, PasswordHash);
            Name = name;
            Email = email;
            MarkAsModified(modifiedBy);
        }

        public void ChangePassword(string newPasswordHash, string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(newPasswordHash),
                "A senha não pode ser vazia.");
            PasswordHash = newPasswordHash;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string name, string email, string passwordHash)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome é obrigatório.");
            ValidateDomain(name.Length < 5, "O nome deve ter pelo menos 5 caracteres.");

            ValidateDomain(string.IsNullOrWhiteSpace(email), "O e-mail é obrigatório.");
            ValidateDomain(!email.Contains('@'), "O e-mail deve ser válido.");

            ValidateDomain(string.IsNullOrWhiteSpace(passwordHash), "A senha é obrigatória.");
        }
    }
}

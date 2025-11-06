using Domain.Enums;
using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Avatar { get; private set; } = string.Empty;
        public bool IsVerified { get; private set; } = false;
        public UserStatus Status { get; private set; }

        public ICollection<Recipe> Recipes { get; private set; } = new List<Recipe>();
        public ICollection<RecipeLike> RecipeLikes { get; private set; } = new List<RecipeLike>();
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        public ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

        private User() { }

        public User(string name, string email, string passwordHash, string avatar, bool isVerified, UserStatus status, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateAndSetProperties(name, email, passwordHash, avatar, isVerified, status);
        }

        public User(int id, string name, string email, string passwordHash, string avatar, bool isVerified, UserStatus status, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            ValidateAndSetProperties(name, email, passwordHash, avatar, isVerified, status);
            Id = id;
        }

        public void UpdateProfile(string name, string email, string modifiedBy)
        {
            ValidateName(name);
            ValidateEmail(email);

            Name = name;
            Email = email;
            MarkAsModified(modifiedBy);
        }

        public void ChangePassword(string newPasswordHash, string modifiedBy)
        {
            ValidatePasswordHash(newPasswordHash);
            PasswordHash = newPasswordHash;
            MarkAsModified(modifiedBy);
        }

        public void UpdateAvatar(string avatar, string modifiedBy)
        {
            ValidateAvatar(avatar);
            Avatar = avatar;
            MarkAsModified(modifiedBy);
        }

        public void ChangeStatus(UserStatus status, string modifiedBy)
        {
            Status = status;
            MarkAsModified(modifiedBy);
        }

        public void VerifyAccount(string modifiedBy)
        {
            IsVerified = true;
            MarkAsModified(modifiedBy);
        }

        private void ValidateAndSetProperties(string name, string email, string passwordHash, string avatar, bool isVerified, UserStatus status)
        {
            ValidateName(name);
            ValidateEmail(email);
            ValidatePasswordHash(passwordHash);
            ValidateAvatar(avatar);

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Avatar = avatar;
            IsVerified = isVerified;
            Status = status;
        }

        private static void ValidateName(string name)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(name), "O nome é obrigatório.");
            var trimmedName = name.Trim();
            ValidateDomain(trimmedName.Length < 3, "O nome deve ter pelo menos 3 caracteres.");
            ValidateDomain(trimmedName.Length > 100, "O nome deve ter no máximo 100 caracteres.");
            ValidateDomain(!Regex.IsMatch(trimmedName, @"^[\p{L}\s']+$"), "O nome deve conter apenas letras, espaços e apóstrofos.");
        }

        private static void ValidateEmail(string email)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(email), "O e-mail é obrigatório.");

            // Verifica se há espaços no email (não permitido)
            ValidateDomain(email.Contains(" "), "O e-mail não pode conter espaços.");

            ValidateDomain(email.Length > 254, "O e-mail deve ter no máximo 254 caracteres.");

            // Regex mais robusta para validação de email
            string emailPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            ValidateDomain(!Regex.IsMatch(email, emailPattern), "O formato do e-mail é inválido.");

            // Validação adicional do domínio
            ValidateDomain(!IsValidEmailDomain(email), "O domínio do e-mail é inválido.");
        }

        private static bool IsValidEmailDomain(string email)
        {
            try
            {
                var parts = email.Split('@');
                if (parts.Length != 2)
                    return false;

                var domain = parts[1];

                // Verifica se o domínio tem pelo menos um ponto
                if (!domain.Contains('.'))
                    return false;

                // Verifica se o domínio não termina com ponto
                if (domain.EndsWith("."))
                    return false;

                // Verifica comprimento do domínio
                if (domain.Length < 4 || domain.Length > 253)
                    return false;

                // Verifica se há espaços no domínio
                if (domain.Contains(" "))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void ValidatePasswordHash(string passwordHash)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(passwordHash), "A senha é obrigatória.");
            ValidateDomain(passwordHash.Length < 8, "A senha deve ter pelo menos 8 caracteres.");
            ValidateDomain(passwordHash.Length > 500, "O hash da senha é muito longo.");
        }

        private static void ValidateAvatar(string avatar)
        {
            if (string.IsNullOrWhiteSpace(avatar))
                return;

            ValidateDomain(avatar.Length > 500, "A URL do avatar deve ter no máximo 500 caracteres.");

            // Validação opcional para URL se necessário
            if (avatar.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                ValidateDomain(!Uri.TryCreate(avatar, UriKind.Absolute, out _), "A URL do avatar é inválida.");
            }
        }
    }
}
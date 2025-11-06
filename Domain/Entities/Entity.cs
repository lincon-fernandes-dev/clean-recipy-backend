using Domain.Validation;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string CreatedBy { get; protected set; } = string.Empty;
        public string LastModifiedBy { get; protected set; } = string.Empty;

        protected Entity() { }

        protected Entity(DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
        {
            ValidateDates(createdAt, updatedAt);
            ValidateAuditFields(createdBy, lastModifiedBy);

            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public void MarkAsModified(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "O usuário modificador é obrigatório.");
            ValidateDomain(modifiedBy != modifiedBy.Trim(), "O usuário modificador não pode conter espaços no início ou fim.");
            ValidateDomain(modifiedBy.Length > 100, "O usuário modificador deve ter no máximo 100 caracteres.");

            LastModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        protected static void ValidateDomain(bool condition, string errorMessage)
        {
            if (condition)
                throw new DomainExceptionValidation(errorMessage);
        }

        protected static void ValidateDates(DateTime createdAt, DateTime updatedAt)
        {
            ValidateDomain(createdAt > DateTime.UtcNow, "A data de criação não pode ser no futuro.");
            ValidateDomain(updatedAt > DateTime.UtcNow, "A data de modificação não pode ser no futuro.");
            ValidateDomain(updatedAt < createdAt, "A data de modificação não pode ser anterior à data de criação.");
        }

        protected static void ValidateAuditFields(string createdBy, string lastModifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(createdBy), "O usuário criador é obrigatório.");
            ValidateDomain(string.IsNullOrWhiteSpace(lastModifiedBy), "O usuário modificador é obrigatório.");

            // Verifica se há espaços no início ou fim (não permitido)
            ValidateDomain(createdBy != createdBy.Trim(), "O usuário criador não pode conter espaços no início ou fim.");
            ValidateDomain(lastModifiedBy != lastModifiedBy.Trim(), "O usuário modificador não pode conter espaços no início ou fim.");

            ValidateDomain(createdBy.Length > 100, "O usuário criador deve ter no máximo 100 caracteres.");
            ValidateDomain(lastModifiedBy.Length > 100, "O usuário modificador deve ter no máximo 100 caracteres.");
        }
    }
}
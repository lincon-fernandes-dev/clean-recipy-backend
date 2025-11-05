using Domain.Validation;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
        public string CreatedBy { get; protected set; } = string.Empty;
        public string LastModifiedBy { get; protected set; } = string.Empty;

        public void MarkAsModified(string modifiedBy)
        {
            LastModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        protected static void ValidateDomain(bool condition, string errorMessage)
        {
            if (condition)
                throw new DomainExceptionValidation(errorMessage);
        }
    }
}

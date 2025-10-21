using Domain.Validation;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; protected set; } = DateTime.UtcNow;
        public string CreatedBy { get; protected set; } = string.Empty;
        public string LastModifiedBy { get; protected set; } = string.Empty;

        public void MarkAsModified(string modifiedBy)
        {
            LastModifiedBy = modifiedBy;
            LastModifiedDate = DateTime.UtcNow;
        }

        protected static void ValidateDomain(bool condition, string errorMessage)
        {
            if (condition)
                throw new DomainExceptionValidation(errorMessage);
        }
    }

}

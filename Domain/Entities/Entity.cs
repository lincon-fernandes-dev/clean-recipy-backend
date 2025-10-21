using Domain.Validation;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; protected set; } = DateTime.UtcNow;
        public string CreatedBy { get; protected set; } = string.Empty;
        public string ModifiedBy { get; protected set; } = string.Empty;

        public void MarkAsModified(string modifiedBy)
        {
            ModifiedBy = modifiedBy;
            ModifiedDate = DateTime.UtcNow;
        }

        protected static void ValidateDomain(bool condition, string errorMessage)
        {
            if (condition)
                throw new DomainExceptionValidation(errorMessage);
        }
    }

}

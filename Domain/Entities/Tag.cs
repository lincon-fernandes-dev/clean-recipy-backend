namespace Domain.Entities
{
    public class Tag : Entity
    {
        public string Title { get; private set; } = string.Empty;
        public IEnumerable<RecipeTag> RecipeTags { get; private set; } = new List<RecipeTag>();

        private Tag() { }

        public Tag(string title, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(title);
            Title = title.Trim();
        }

        public Tag(int id, string title, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(title);

            Id = id;
            Title = title.Trim();
        }

        public void UpdateTitle(string newTitle, string modifiedBy)
        {
            Validate(newTitle);
            Title = newTitle.Trim();
            MarkAsModified(modifiedBy);
        }

        private static void Validate(string title)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(title), "O título da tag é obrigatório.");

            var trimmedTitle = title.Trim();
            ValidateDomain(trimmedTitle.Length < 2, "O título da tag deve ter pelo menos 2 caracteres.");
            ValidateDomain(trimmedTitle.Length > 128, "O título da tag deve ter no máximo 128 caracteres.");

            // Validação adicional para caracteres inválidos
            ValidateDomain(!System.Text.RegularExpressions.Regex.IsMatch(trimmedTitle, @"^[\p{L}\p{N}\s\-_]+$"),
                "O título da tag deve conter apenas letras, números, espaços, hífens e underscores.");
        }

        // Método para normalizar o título (útil para comparações)
        public string GetNormalizedTitle()
        {
            return Title.Trim().ToLowerInvariant();
        }

        // Override do Equals para comparar tags pelo título normalizado
        public override bool Equals(object obj)
        {
            if (obj is Tag other)
            {
                return GetNormalizedTitle() == other.GetNormalizedTitle();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return GetNormalizedTitle().GetHashCode();
        }
    }
}
namespace Domain.Entities
{
    public class RecipeTag : Entity
    {
        public int IdTag { get; private set; }
        public int IdRecipe { get; private set; }
        public Recipe Recipe { get; private set; }
        public Tag Tag { get; private set; }

        private RecipeTag() { }

        public RecipeTag(int idTag, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(idTag, idRecipe);
            IdTag = idTag;
            IdRecipe = idRecipe;
        }

        public RecipeTag(int id, int idTag, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(idTag, idRecipe);

            Id = id;
            IdTag = idTag;
            IdRecipe = idRecipe;
        }

        public void UpdateRecipeTag(int newIdTag, int newIdRecipe, string modifiedBy)
        {
            Validate(newIdTag, newIdRecipe);
            IdTag = newIdTag;
            IdRecipe = newIdRecipe;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(int idTag, int idRecipe)
        {
            ValidateDomain(idTag < 1, "Id da tag inválido. Id deve ser um número inteiro e positivo.");
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
        }

        public override bool Equals(object obj)
        {
            if (obj is RecipeTag other)
            {
                return IdTag == other.IdTag && IdRecipe == other.IdRecipe;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdTag, IdRecipe);
        }
    }
}
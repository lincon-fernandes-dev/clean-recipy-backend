using Domain.Validation;

namespace Domain.Entities
{
    public class RecipeLike : Entity
    {
        public int IdRecipe { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
        public Recipe Recipe { get; private set; }

        private RecipeLike() { }

        public RecipeLike(int idUser, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(idUser, idRecipe);
            IdUser = idUser;
            IdRecipe = idRecipe;
        }

        public RecipeLike(int id, int idUser, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(idUser, idRecipe);

            Id = id;
            IdUser = idUser;
            IdRecipe = idRecipe;
        }

        public void UpdateRecipeLike(int newIdUser, int newIdRecipe, string modifiedBy)
        {
            Validate(newIdUser, newIdRecipe);
            IdUser = newIdUser;
            IdRecipe = newIdRecipe;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(int idUser, int idRecipe)
        {
            ValidateDomain(idUser < 1, "Id do usuário inválido. Id deve ser um número inteiro e positivo.");
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
        }

        public override bool Equals(object obj)
        {
            if (obj is RecipeLike other)
            {
                return IdUser == other.IdUser && IdRecipe == other.IdRecipe;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdUser, IdRecipe);
        }
    }
}
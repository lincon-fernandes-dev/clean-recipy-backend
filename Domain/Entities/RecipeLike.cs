namespace Domain.Entities
{
    public class RecipeLike : Entity
    {
        public int IdRecipe { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
        public Recipe Recipe { get; private set; }
        public RecipeLike() { }
        public RecipeLike(int idRecipeLike, int idUser, int idRecipe)
        {
            ValidateDomain(idRecipeLike < 1, "Id de usuario inválido, Id deve ser um numero inteiro e positivo");
            Validate(idUser, idRecipe);
            IdRecipe = idRecipeLike;

            IdUser = idUser;
            IdRecipe = idRecipe;
        }
        public RecipeLike(int idUser, int idRecipe)
        {
            Validate(idUser, idRecipe);

            IdUser = idUser;
            IdRecipe = idRecipe;
        }
        public static void Validate(int idUser, int idRecipe)
        {
            ValidateDomain(idUser < 1, "Id de usuario inválido, Id deve ser um numero inteiro e positivo");
            ValidateDomain(idRecipe < 1, "Id da receita inválido, Id deve ser um numero inteiro e positivo");
        }
    }
}

using Domain.Enums;

namespace Domain.Entities
{
    public sealed class RecipeIngredient : Entity
    {
        public int RecipeId { get; private set; }

        public int IngredientId { get; private set; }
        public Ingredient Ingredient { get; private set; } = null!;

        public decimal Quantity { get; private set; }
        public UnidadeMedidaEnum UnidadeMedida { get; private set; } = UnidadeMedidaEnum.NaoInformada;

        private RecipeIngredient() { }

        public RecipeIngredient(
            int recipeId,
            int ingredientId,
            decimal quantity,
            UnidadeMedidaEnum unidadeMedida,
            string createdBy)
        {
            Validate(recipeId, ingredientId, quantity, unidadeMedida, createdBy);

            RecipeId = recipeId;
            IngredientId = ingredientId;
            Quantity = quantity;
            UnidadeMedida = unidadeMedida;
            CreatedBy = createdBy;
        }

        public RecipeIngredient(
            int id,
            int recipeId,
            int ingredientId,
            decimal quantity,
            UnidadeMedidaEnum unidadeMedida,
            string createdBy)
        {
            Validate(recipeId, ingredientId, quantity, unidadeMedida, createdBy);
            ValidateDomain(id < 1, "Id inválido, Id deve ser um numero inteiro e positivo");

            Id = id;
            RecipeId = recipeId;
            IngredientId = ingredientId;
            Quantity = quantity;
            UnidadeMedida = unidadeMedida;
            CreatedBy = createdBy;
        }

        public void UpdateQuantity(decimal newQuantity, string modifiedBy)
        {
            Validate(RecipeId, IngredientId, newQuantity, UnidadeMedida, CreatedBy);

            Quantity = newQuantity;

            MarkAsModified(modifiedBy);
        }
        
        private static void Validate(int recipeId, int ingredientId, decimal quantity, UnidadeMedidaEnum unidadeMedida, string createdBy)
        {
            ValidateDomain(recipeId < 1, "Id inválido, Id da receita deve ser um numero inteiro e positivo");

            ValidateDomain(ingredientId < 1, "Id inválido, Id do ingrediente deve ser um numero inteiro e positivo");

            ValidateDomain(quantity <= 0, "A quantidade deve ser maior que zero.");

            ValidateDomain(!Enum.IsDefined(typeof(UnidadeMedidaEnum), unidadeMedida),
                "Unidade de medida inválida.");

            ValidateDomain(string.IsNullOrEmpty(createdBy), "O nome de usuario é obrigatorio");
            ValidateDomain(createdBy.Length < 4, "O nome de usuario deve ter pelo menos 4 caracteres");
        }
    }
}

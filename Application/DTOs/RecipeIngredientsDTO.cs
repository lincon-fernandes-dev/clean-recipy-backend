using Domain.Enums;

namespace Application.DTOs
{
    public class RecipeIngredientsDTO
    {
        public int Id {  get; set; }
        public int RecipeId { get; set; }
        public required IngredientDTO Ingredient { get; set; }
        public Decimal Quantity { get; set; }
        public UnidadeMedidaEnum UnidadeMedidaEnum { get; set; }
    }
}

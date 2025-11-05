using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RecipeTag : Entity
    {
        public int IdTag { get; private set; }
        public int IdRecipe { get; private set; }
        public Recipe Recipe { get; private set; }
        public Tag Tag { get; private set; }
        public RecipeTag() { }
        public RecipeTag(int idTag, int idRecipe)
        {
            Validate(idTag, idRecipe);

            IdTag = idTag;
            IdRecipe = idRecipe;
        }
        public static void Validate(int idTag, int idRecipe)
        {
            ValidateDomain(idTag < 1, "id Tag invalido, id deve ser um numero inteiro positivo");
        }
    }
}

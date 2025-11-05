using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tag : Entity
    {
        public string Title { get; private set; } = string.Empty;

        public IEnumerable<RecipeTag> RecipeTags { get; private set; }
        public Tag() { }
        public Tag(string description)
        {
            Validate(description);
            Title = description;
        }
        public Tag(int idTag, string description)
        {
            ValidateDomain(idTag < 1, "Id invalido, id deve ser um inteiro positivo");
            Validate(description);
        }
        public static void Validate(string description)
        {
            ValidateDomain(description.Length < 2, "descrição invalida. minimo dois caracteres");
            ValidateDomain(description.Length > 128, "descrição invalida. maximo 128 caracteres");
        }
    }
}

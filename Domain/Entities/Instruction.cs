using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Instruction : Entity
    {
        public int IdRecipe { get; private set; }
        public string Content {  get; private set; } = string.Empty;
        public Recipe Recipe { get; private set; }
        public Instruction() { }
        public Instruction(int idRecipe, string content)
        {
            Validade(idRecipe, content);
            IdRecipe = idRecipe;
            Content = content;
        }
        public Instruction(int idInstruction, int idRecipe, string content)
        {
            ValidateDomain(idInstruction < 0, "Id invalido, id deve ser um numero inteiro positivo");
            Validade(idRecipe, content);

        }

        public static void Validade(int idRecipe, string content)
        {
            ValidateDomain(idRecipe < 1, "id da receita invalido, id deve ser um numero inteiro positivo");
            ValidateDomain(content.Length < 8, "A instrução deve conter ao menos 8 caracteres");

        }
    }
}

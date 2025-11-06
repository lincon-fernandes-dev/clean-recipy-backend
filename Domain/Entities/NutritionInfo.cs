using Domain.Validation;

namespace Domain.Entities
{
    public class NutritionInfo : Entity
    {
        public int IdRecipe { get; private set; }
        public int Calories { get; private set; }
        public int Proteins { get; private set; }
        public int Carbs { get; private set; }
        public int Fat { get; private set; }
        public Recipe Recipe { get; private set; }

        private NutritionInfo() { }

        public NutritionInfo(int idRecipe, int calories, int proteins, int carbs, int fat, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(idRecipe, calories, proteins, carbs, fat);

            IdRecipe = idRecipe;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
        }

        public NutritionInfo(int id, int idRecipe, int calories, int proteins, int carbs, int fat, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(idRecipe, calories, proteins, carbs, fat);

            Id = id;
            IdRecipe = idRecipe;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
        }

        public void UpdateNutritionInfo(int calories, int proteins, int carbs, int fat, string modifiedBy)
        {
            Validate(IdRecipe, calories, proteins, carbs, fat);
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
            MarkAsModified(modifiedBy);
        }

        public void UpdateRecipe(int newIdRecipe, string modifiedBy)
        {
            ValidateDomain(newIdRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
            IdRecipe = newIdRecipe;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(int idRecipe, int calories, int proteins, int carbs, int fat)
        {
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
            ValidateDomain(calories < 0, "Calorias não podem ser negativas.");
            ValidateDomain(proteins < 0, "Proteínas não podem ser negativas.");
            ValidateDomain(carbs < 0, "Carboidratos não podem ser negativos.");
            ValidateDomain(fat < 0, "Gorduras não podem ser negativas.");

            // Validações adicionais para valores máximos razoáveis
            ValidateDomain(calories > 10000, "Calorias não podem exceder 10000.");
            ValidateDomain(proteins > 1000, "Proteínas não podem exceder 1000g.");
            ValidateDomain(carbs > 2000, "Carboidratos não podem exceder 2000g.");
            ValidateDomain(fat > 1000, "Gorduras não podem exceder 1000g.");
        }

        public int GetTotalCalories()
        {
            return Calories;
        }

        public string GetNutritionSummary()
        {
            return $"Calorias: {Calories}kcal, Proteínas: {Proteins}g, Carboidratos: {Carbs}g, Gorduras: {Fat}g";
        }
    }
}
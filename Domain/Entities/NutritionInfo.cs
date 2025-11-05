using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NutritionInfo : Entity
    {
        public int IdRecipe { get; private set; }
        public int Calories { get; private set; }
        public int Proteins { get; private set; }
        public int Carbs { get; private set; }
        public int Fat {  get; private set; }
        public Recipe Recipe { get; private set; }

        public NutritionInfo() { }
        public NutritionInfo(int idRecipe, int calories, int proteins, int carbs, int fat)
        {
            Validate(idRecipe, calories, proteins, carbs, fat);

            IdRecipe = idRecipe;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
        }
        public NutritionInfo(int idNutritionInfo, int idRecipe, int calories, int proteins, int carbs, int fat)
        {
            ValidateDomain(idNutritionInfo < 1, "idNutritionInfo invalido, idNutritionInfo deve ser um numero inteiro positivo");
            Validate(idRecipe, calories, proteins, carbs, fat);

            Id = idNutritionInfo;
            IdRecipe = idRecipe;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fat = fat;
        }
        public static void Validate(int idRecipe, int calories, int proteins, int carbs, int fat)
        {
            ValidateDomain(idRecipe < 1, "Id invalido, id deve ser um numero inteiro positivo");
            ValidateDomain(calories < 1, "Id calorias, id deve ser um numero inteiro positivo");
            ValidateDomain(proteins < 1, "proteins invalido, proteins deve ser um numero inteiro positivo");
            ValidateDomain(carbs < 1, "carbs invalido, carbs deve ser um numero inteiro positivo");
            ValidateDomain(fat < 1, "fat invalido, fat deve ser um numero inteiro positivo");

        }
    }
}

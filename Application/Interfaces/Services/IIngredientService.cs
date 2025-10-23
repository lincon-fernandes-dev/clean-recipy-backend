using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDTO>?> GetAll();
        Task<IEnumerable<IngredientDTO>?> GetByName(string name);
        Task<IngredientDTO?> Create(IngredientDTO ingredient);
        Task<IngredientDTO?> Update(IngredientDTO ingredient);
        Task<IngredientDTO?> Delete(IngredientDTO ingredient);
    }
}

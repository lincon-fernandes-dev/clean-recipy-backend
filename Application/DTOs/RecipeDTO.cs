using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int UserId { get; set; }
        public required UserDTO UserDTO { get; set; }
        public ICollection<RecipeIngredientsDTO> RecipeIngredientDTOs { get; set; } = [];
        public ICollection<VoteDTO> VoteDTOs { get; set; } = [];

    }
}

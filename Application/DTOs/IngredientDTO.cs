using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

    }
}

using Ecommerce.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class AddCartItemDto
    {
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be at least 1.")]
        public int Quantity { get; set; }
    }
}

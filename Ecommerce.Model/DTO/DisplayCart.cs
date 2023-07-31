using Ecommerce.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class DisplayCart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public ICollection<DisplayCartItemDto> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalWeight { get; set; }
    }
}

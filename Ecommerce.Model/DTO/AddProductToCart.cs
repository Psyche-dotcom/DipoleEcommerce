using Ecommerce.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class AddProductToCart
    {
        public string ProductId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.Entities
{
    public class CartItem: BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        
    }
}

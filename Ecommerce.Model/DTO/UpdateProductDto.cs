﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableProductNumber { get; set; }
    }
}

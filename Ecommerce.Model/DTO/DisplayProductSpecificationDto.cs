using Ecommerce.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public  class DisplayProductSpecificationDto
    {
        public string ProductId { get; set; }
        public string SKU { get; set; }
        public string Model { get; set; }
        public decimal Weight { get; set; }
        public string ShopType { get; set; }
        public string Color { get; set; }
        public string KeyFeature { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class DisplayOrderDto
    {
        public string Id { get; set; }
        public long DeliveryFee { get; set; }
        public long ItemTotalPrice { get; set; }
        public long OverallPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class DisplayDeliveryCharges
    {
        public string CourierName { get; set; }
        public string CourierFee { get; set; }
        public decimal GoodWight { get; set; }
    }
}

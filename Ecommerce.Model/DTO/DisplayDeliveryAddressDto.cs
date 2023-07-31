using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.DTO
{
    public class DisplayDeliveryAddressDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverCountry { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverCity { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model.Entities
{
    public class Delivery : BaseEntity
    {
        public string SenderName { get; set; } = "DipoleEcommerce";
        public string SenderCountry { get; set; } = "Nigeria";
        public string SenderPhone { get; set; } = "08160250471";
        public string SenderStreet { get; set; } = "Alawe Street, Ifako, Gbagada";
        public string SenderState { get; set; } = "Lagos";
        public string SenderCity { get; set; } = "Ikeja";
        public string SenderPostalCode { get; set; } = "100211";
        public string ReceiverName { get; set; }
        public string ReceiverCountry { get; set; } = "Nigeria";
        public string ReceiverPhone { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverPostalCode { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}

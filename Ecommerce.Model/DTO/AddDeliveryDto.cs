using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Model.DTO
{
   public class AddDeliveryDto
    {
        [Required]
        [Phone]
        public string MobilePhone { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}

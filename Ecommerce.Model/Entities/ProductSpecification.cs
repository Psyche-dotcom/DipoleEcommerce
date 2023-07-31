using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Model.Entities
{
    public class ProductSpecification : BaseEntity
    {
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string SKU { get; set; }
        public string Model { get; set; }
        public decimal Weight { get; set; }
        public string ShopType { get; set; }
        public string Color { get; set; }
        public string KeyFeature { get; set; }
    }
}

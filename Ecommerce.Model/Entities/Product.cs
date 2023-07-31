using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.Entities
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableProductNumber { get; set; }
        public string? SpecificationId { get; set; } = string.Empty;
        public int ProductRating { get; set; } = 0;
        public int NoofView { get; set; } = 0;
        public ProductSpecification Specification { get; set; }
        public ICollection<WishListItem> Wishlists { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public ICollection<ProductRating> Ratings { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }
}

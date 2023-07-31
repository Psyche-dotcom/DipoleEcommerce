using Ecommerce.Model.Enum;
using Microsoft.AspNetCore.Identity;

using System.Diagnostics.Metrics;

namespace Ecommerce.Model.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string? CartId { get; set; }
        public Cart Cart { get; set; }
        public string? DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        public string? WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
        public ICollection<ProductRating> ProductRating { get; set; }
        public ICollection<ProductReview> ProductReview { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

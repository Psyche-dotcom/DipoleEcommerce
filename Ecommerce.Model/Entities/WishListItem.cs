using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.Entities
{
    public class WishListItem :BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}

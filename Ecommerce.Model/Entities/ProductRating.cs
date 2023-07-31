using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Model.Entities
{
    public class ProductRating: BaseEntity
    {
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductReviewId { get; set; }
        public ProductReview ProductReview { get; set; }
    }
}

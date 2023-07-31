namespace Ecommerce.Model.Entities
{
    public class ProductReview : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductRatingId { get; set; }
        public ProductRating ProductRating { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}

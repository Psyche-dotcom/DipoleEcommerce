namespace Ecommerce.Model.DTO
{
    public class DisplayProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public int AvailableProductNumber { get; set; }
        public string SpecificationId { get; set; }
        public int NoofView { get; set; }
        public int ProductRating { get; set; }
        public DisplayProductSpecificationDto Specification { get; set; }

    }
}

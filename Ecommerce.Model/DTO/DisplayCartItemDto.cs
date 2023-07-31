namespace Ecommerce.Model.DTO
{
    public class DisplayCartItemDto
    {
        public string Id { get; set; }
        public DisplayProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}

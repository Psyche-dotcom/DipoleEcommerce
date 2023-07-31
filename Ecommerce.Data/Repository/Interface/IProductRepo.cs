using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;

namespace Ecommerce.Data.Repository.Interface
{
    public interface IProductRepo
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductbyIdAsync(string productId);
        Task<PaginatedDto<DisplayProduct>> SearchProductsAsync(decimal? minPrice, decimal? maxPrice, string searchTerm, int pageNumber);
        Task<ProductSpecification> AddProductSpecificaton(ProductSpecification productSpecification);
        Task<bool> DeleteProductAsync(Product product);
        Task<Product> UpdateProductDetailsAsync(Product product);
        Task<PaginatedDto<DisplayProduct>> GetAllProductsAsync(int pageNumber, int perPageSize);

    }
}

using Ecommerce.Model.DTO;

namespace Emmerce.API.Service.Interface
{
    public interface IProductService
    {
        Task<ResponseDto<DisplayProductDto>> AddProductAsync(AddProductDto product);
        Task<ResponseDto<DisplayProductDto>> AddProductSpecificationAsync(AddProductSpecificationDto productSpec);
        Task<ResponseDto<DisplayProductDto>> GetProductProductbyIdAsync(string productId); Task<ResponseDto<PaginatedDto<DisplayProduct>>> GetAllProductAsync(int perPageSize, int pageNumber);
        Task<ResponseDto<PaginatedDto<DisplayProduct>>> SearchProductAsync(decimal? minPrice, decimal? maxPrice, string searchTerm, int pageNumber);
        Task<ResponseDto<string>> DeleteProductbyIdAsync(string productId);
        Task<ResponseDto<DisplayProduct>> UpdateProductAsync(string productid, UpdateProductDto productDto);
    }
}

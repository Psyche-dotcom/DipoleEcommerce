using AutoMapper;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;
using Emmerce.API.Service.Interface;

namespace Emmerce.API.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepo _productRepo;

        public ProductService(IMapper mapper, ILogger<ProductService> logger, IProductRepo productRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepo = productRepo;
        }
        public async Task<ResponseDto<DisplayProductDto>> AddProductAsync(AddProductDto product)
        {
            var response = new ResponseDto<DisplayProductDto>();
            try
            {
                var MappProduct = _mapper.Map<Product>(product);
                var AddProduct = await _productRepo.CreateProductAsync(MappProduct);
                if (AddProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in adding product" };
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var displayProduct = _mapper.Map<DisplayProductDto>(AddProduct);
                response.DisplayMessage = "Successfully added product";
                response.StatusCode = StatusCodes.Status201Created;
                response.Result = displayProduct;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in adding product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayProductDto>> AddProductSpecificationAsync(AddProductSpecificationDto productSpec)
        {
            var response = new ResponseDto<DisplayProductDto>();
            try
            {
                var checkProduct = await _productRepo.GetProductbyIdAsync(productSpec.ProductId);
                if (checkProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Product not found" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var MappProductSpec = _mapper.Map<ProductSpecification>(productSpec);
                var AddProductSpec = await _productRepo.AddProductSpecificaton(MappProductSpec);
                if (AddProductSpec == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in adding product specification" };
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Error";
                    return response;
                }
                checkProduct.SpecificationId = AddProductSpec.Id;
                var updateProduct = await _productRepo.UpdateProductDetailsAsync(checkProduct);
                if (updateProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating product details" };
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var displayProduct = _mapper.Map<DisplayProductDto>(checkProduct);
                response.DisplayMessage = "Successfully added product specification";
                response.StatusCode = StatusCodes.Status201Created;
                response.Result = displayProduct;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in adding product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayProductDto>> GetProductProductbyIdAsync(string productId)
        {
            var response = new ResponseDto<DisplayProductDto>();
            try
            {
                var checkProduct = await _productRepo.GetProductbyIdAsync(productId);
                if (checkProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Product not found" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                checkProduct.NoofView += 1;
                await _productRepo.UpdateProductDetailsAsync(checkProduct);
                var displayProduct = _mapper.Map<DisplayProductDto>(checkProduct);
                response.DisplayMessage = "Success";
                response.StatusCode = StatusCodes.Status200OK; ;
                response.Result = displayProduct;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in retrieving product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }

        }
        public async Task<ResponseDto<string>> DeleteProductbyIdAsync(string productId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkProduct = await _productRepo.GetProductbyIdAsync(productId);
                if (checkProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Product not found" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var deleteProduct = await _productRepo.DeleteProductAsync(checkProduct);
                if(deleteProduct == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in deleting product" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                
                response.DisplayMessage = "Success";
                response.StatusCode = StatusCodes.Status200OK;
                response.Result = "Product successfully deleted";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in deleting product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }

        }
        public async Task<ResponseDto<PaginatedDto<DisplayProduct>>> GetAllProductAsync(int perPageSize, int pageNumber)
        {
            var response = new ResponseDto<PaginatedDto<DisplayProduct>>();
            try
            {
                var paginatedProduct = await _productRepo.GetAllProductsAsync(pageNumber, perPageSize);
                if (paginatedProduct.Result.Any())
                {
                    response.Result = paginatedProduct;
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    return response;
                }
                response.Result = paginatedProduct;
                response.StatusCode = 200;
                response.DisplayMessage = "There is no more product";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in retrieving product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<DisplayProduct>> UpdateProductAsync(string productid, UpdateProductDto productDto)
        {
            var response = new ResponseDto<DisplayProduct>();
            try
            {
                var checkProduct = await _productRepo.GetProductbyIdAsync(productid);
                if (checkProduct == null)
                {
                    response.ErrorMessages = new List<string>() { "Product not found" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var automap = _mapper.Map(productDto, checkProduct);
                var update = await _productRepo.UpdateProductDetailsAsync(automap);
                if (update == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating product" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var automapResult = _mapper.Map<DisplayProduct>(update);
                response.DisplayMessage = "Success";
                response.StatusCode = 200;
                response.Result = automapResult;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<PaginatedDto<DisplayProduct>>> SearchProductAsync(decimal? minPrice, decimal? maxPrice, string searchTerm, int pageNumber)
        {
            var response = new ResponseDto<PaginatedDto<DisplayProduct>>();
            try
            {
                var paginatedProduct = await _productRepo.SearchProductsAsync(minPrice, maxPrice, searchTerm, pageNumber);
                if (paginatedProduct.Result.Any())
                {
                    response.Result = paginatedProduct;
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    return response;
                }
                response.Result = paginatedProduct;
                response.StatusCode = 404;
                response.ErrorMessages = new List<string>() { "There is no product with the search parameter" };
                response.DisplayMessage = "Error";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in retrieving product" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
    }
}

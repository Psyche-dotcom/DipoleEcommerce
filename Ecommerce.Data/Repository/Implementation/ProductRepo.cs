using Ecommerce.Data.Context;
using Ecommerce.Data.Repository.Interface;
using Ecommerce.Model.DTO;
using Ecommerce.Model.Entities;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data.Repository.Implementation
{
    public class ProductRepo : IProductRepo
    {
        private readonly DipoleEcommerceContext _context;

        public ProductRepo(DipoleEcommerceContext context)
        {
            _context = context;
        }

        public async Task<ProductSpecification> AddProductSpecificaton(ProductSpecification productSpecification)
        {
            var addProductSpecification = await _context.ProductSpecifications.AddAsync(productSpecification);
            await _context.SaveChangesAsync();
            return addProductSpecification.Entity;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var addProduct = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return addProduct.Entity;
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<PaginatedDto<DisplayProduct>> GetAllProductsAsync(int pageNumber, int perPageSize)
        {
            var result = new PaginatedDto<DisplayProduct>();
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var totalCount = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = await _context.Products.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).Select(Prod => new DisplayProduct
            {
                AvailableProductNumber = Prod.AvailableProductNumber,
                Brand = Prod.Brand,
                Price = Prod.Price,
                Description = Prod.Description,
                Id = Prod.Id,
                ImgUrl = Prod.ImgUrl,
                Name = Prod.Name,
                NoofView = Prod.NoofView,
                ProductRating = Prod.ProductRating

            }).ToListAsync();
            result.TotalPages = totalPages;
            result.CurrentPage = pageNumber;
            result.PageSize = perPageSize;
            result.Result = paginated;
            return result;
        }

        public async Task<Product> GetProductbyIdAsync(string productId)
        {
            var product = await _context.Products.Include(spec => spec.Specification).Include(e => e.Reviews).FirstOrDefaultAsync(prod => prod.Id == productId);
            return product;
        }
        public async Task<PaginatedDto<DisplayProduct>> SearchProductsAsync(decimal? minPrice, decimal? maxPrice, string searchTerm, int pageNumber)
        {
            var result = new PaginatedDto<DisplayProduct>();
            var query = _context.Products.AsQueryable();

            if (minPrice != null)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Brand.Contains(searchTerm));
            }

            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            var perPageSize = 5;
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = await query.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).Select(Prod => new DisplayProduct
            {
                AvailableProductNumber = Prod.AvailableProductNumber,
                Brand = Prod.Brand,
                Price = Prod.Price,
                Description = Prod.Description,
                Id = Prod.Id,
                ImgUrl = Prod.ImgUrl,
                Name = Prod.Name,
                NoofView = Prod.NoofView,
                ProductRating = Prod.ProductRating
            }).ToListAsync();
            result.TotalPages = totalPages;
            result.CurrentPage = pageNumber;
            result.PageSize = perPageSize;
            result.Result = paginated;
            return result;
        }

        public async Task<Product> UpdateProductDetailsAsync(Product product)
        {
            var update = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return update.Entity;
        }
    }
}

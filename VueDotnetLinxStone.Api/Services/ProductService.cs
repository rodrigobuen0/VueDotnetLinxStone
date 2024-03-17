using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VueDotnetLinxStone.Api.Entities;

namespace VueDotnetLinxStone.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(ProductCreateDto product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task DeleteProductAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFakeStoreApiClient _fakeStoreApiClient;

        public ProductService(ApplicationDbContext dbContext, IFakeStoreApiClient fakeStoreApiClient)
        {
            _dbContext = dbContext;
            _fakeStoreApiClient = fakeStoreApiClient;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> AddProductAsync(ProductCreateDto productDto)
        {
            var product = new Product
            {
                Title = productDto.Title,
                Price = productDto.Price,
                BarCode = productDto.BarCode,
                Image = productDto.Image
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var fakeStoreProductDto = new FakeStoreProductDto
            {
                title = productDto.Title,
                price = productDto.Price,
                image = productDto.Image,
                description = productDto.Title,
                category = "N/A"
            };

            await _fakeStoreApiClient.AddProductAsync(fakeStoreProductDto);
            return product;
        }

        public async Task<Product> UpdateProductAsync(int id, Product productDto)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return null;

            product.Title = productDto.Title;
            product.Price = productDto.Price;
            product.BarCode = productDto.BarCode;
            product.Image = productDto.Image;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            var fakeStoreProductDto = new FakeStoreProductDto
            {
                title = productDto.Title,
                price = productDto.Price,
                image = productDto.Image,
                description = productDto.Title,
                category = "N/A"
            };

            await _fakeStoreApiClient.UpdateProductAsync(id, fakeStoreProductDto);
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
                return;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            await _fakeStoreApiClient.DeleteProductAsync(id);
        }
    }
}

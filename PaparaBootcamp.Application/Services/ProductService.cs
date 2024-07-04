using AutoMapper;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;

namespace PaparaBootcamp.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name, string sortBy)
        {
            return await _productRepository.GetProductsAsync(name, sortBy);
        }

        public async Task<(IEnumerable<Product> items, int totalCount)> GetItemsAsync(int pageIndex, int pageSize)
        {
            return await _productRepository.GetItemsAsync(pageIndex, pageSize);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _productRepository.GetProductAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {

            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}

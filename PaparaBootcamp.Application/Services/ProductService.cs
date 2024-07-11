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

        public async Task<IEnumerable<ProductEntity>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<(IEnumerable<ProductEntity> items, int? totalCount)> GetItemsAsync(int pageIndex, int pageSize,string name , string sortBy)
        {
            return await _productRepository.GetItemsAsync(pageIndex, pageSize, name,sortBy);
        }

        public async Task<ProductEntity> GetProductAsync(int id)
        {
            return await _productRepository.GetItemAsync(id);
        }

        public async Task AddAsync(ProductEntity product)
        {

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}

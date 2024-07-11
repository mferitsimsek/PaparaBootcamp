using MediatR;
using Microsoft.EntityFrameworkCore;
using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;

namespace PaparaBootcamp.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<ProductEntity> _products;

        public ProductRepository()
        {
            _products = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Iphone 15 Pro Max", Price = 82500 },
                new ProductEntity { Id = 2, Name = "Lenovo Thinkpad X1 Carbon Gen11", Price = 117328 },
                new ProductEntity { Id = 3, Name = "Samsung Odyssey Ark G9 OLED", Price = 74999 }
            };
        }

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
            var query= _products.AsQueryable();
            return await Task.FromResult(query);
        }

        public async Task<(List<ProductEntity?> Items, int TotalCount)> GetItemsAsync(int pageIndex, int pageSize, string name, string sortBy)
        {
            var query =  _products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "price":
                        query = query.OrderBy(p => p.Price);
                        break;
                }
            }

            var totalCount =  query.Count();

            var items =  query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();


            return (items, totalCount);
        }

        public async Task<ProductEntity> GetItemAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(product);
        }

        public async Task AddAsync(ProductEntity product)
        {
            _products.Add(product);
             await Task.FromResult(product);
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.CreatedDate = product.CreatedDate;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            await Task.CompletedTask;
        }
    }
}


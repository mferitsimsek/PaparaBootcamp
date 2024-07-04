using PaparaBootcamp.Application.Interfaces;
using PaparaBootcamp.Domain.Entities;

namespace PaparaBootcamp.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Iphone 15 Pro Max", Price = 82500 },
                new Product { Id = 2, Name = "Lenovo Thinkpad X1 Carbon Gen11", Price = 117328 },
                new Product { Id = 3, Name = "Samsung Odyssey Ark G9 OLED", Price = 74999 }
            };
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string name, string sortBy)
        {
            var query = _products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "name" => query.OrderBy(p => p.Name),
                    "price" => query.OrderBy(p => p.Price),
                    _ => query
                };
            }

            return await Task.FromResult(query.ToList());
        }

        public async Task<(IEnumerable<Product>, int)> GetItemsAsync(int pageIndex, int pageSize)
        {
            var items = _products.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            var totalCount = _products.Count;
            return await Task.FromResult((items, totalCount));
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(product);
        }

        public async Task AddProductAsync(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateProductAsync(Product product)
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

        public async Task DeleteProductAsync(int id)
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


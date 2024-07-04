using PaparaBootcamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Interfaces
{
    public interface IProductRepository:IRepository<Product?>
    {
        //Task<IEnumerable<T>> GetProductsAsync(string name, string sortBy);
        //Task<(IEnumerable<T>, int)> GetItemsAsync(int pageIndex, int pageSize);
        //Task<Product> GetProductAsync(int id);
        //Task AddProductAsync(T product);
        //Task UpdateProductAsync(T product);
        //Task DeleteProductAsync(int id);
    }
}

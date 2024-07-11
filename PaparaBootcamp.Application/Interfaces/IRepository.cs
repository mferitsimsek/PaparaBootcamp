using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<(List<T> Items, int TotalCount)> GetItemsAsync(int pageIndex, int pageSize, string name, string sortBy);
        Task<T> GetItemAsync(int id);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
    }
}

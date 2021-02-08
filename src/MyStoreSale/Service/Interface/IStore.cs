using System.Collections.Generic;
using System.Threading.Tasks;
using MyStoreSale.Models;

namespace MyStoreSale.Service.Interface
{
    public interface IStore
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
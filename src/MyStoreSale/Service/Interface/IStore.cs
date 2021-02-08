using System.Collections.Generic;
using System.Threading.Tasks;
using SharedKernel;

namespace MyStoreSale.Service.Interface
{
    public interface IStore
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task ClearProductsCacheAsync();

    }
}
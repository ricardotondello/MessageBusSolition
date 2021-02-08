using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyStoreSale.Service.Interface;

namespace MyStoreSale.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IStore _store;

        public ProductController(ILogger<ProductController> logger, IStore store)
        {
            _logger = logger;
            _store = store;
        }

        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            return View(await _store.GetProductsAsync());
        }
    }
}
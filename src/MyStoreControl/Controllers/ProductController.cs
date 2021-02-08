using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bogus;

namespace MyStoreControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var id = 1;
            var product = new Faker<Product>()
                .RuleFor(o => o.Id, f => id++)
                .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                .RuleFor(o => o.Stock, f => f.Random.Int(1, 100))
                .RuleFor(o => o.Price, f => f.Random.Double(1, 1000));
    
            return product.Generate(new Random().Next(1, 1_100));
        }
    }
}

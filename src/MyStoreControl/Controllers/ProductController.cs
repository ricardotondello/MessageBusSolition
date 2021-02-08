using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bogus;
using MassTransit;
using SharedKernel;

namespace MyStoreControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IPublishEndpoint _publishEndpoint;
        
        public ProductController(ILogger<ProductController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var product = new Faker<Product>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.Name, f => f.Commerce.ProductName())
                .RuleFor(o => o.Stock, f => f.Random.Int(1, 100))
                .RuleFor(o => o.Price, f => f.Random.Double(1, 1000));
    
            return product.Generate(new Random().Next(1, 1_100));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductMessage p)
        {
            await _publishEndpoint.Publish<ProductMessage>(p);
            return Ok();
        }
    }
}

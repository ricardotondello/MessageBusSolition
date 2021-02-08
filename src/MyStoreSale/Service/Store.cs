using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyStoreSale.Models;
using MyStoreSale.Service.Interface;
using Newtonsoft.Json;

namespace MyStoreSale.Service
{
    public class Store : IStore
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public Store(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var resp = await _httpClientFactory.CreateClient().GetAsync(GetURL());
            resp.EnsureSuccessStatusCode();

            var list = JsonConvert.DeserializeObject<IEnumerable<Product>>(await resp.Content.ReadAsStringAsync());

            return await Task.FromResult(list);
        }

        private string GetURL() => _configuration["URL_Products"];
    }
}
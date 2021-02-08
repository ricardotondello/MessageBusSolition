using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using MyStoreSale.Extensions;
using MyStoreSale.Models;
using MyStoreSale.Service.Interface;
using Newtonsoft.Json;

namespace MyStoreSale.Service
{
    public class Store : IStore
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private static IDistributedCache _cache;
        private static string recordKey => nameof(Product) + "_" + DateTime.Now.ToString("yyyyMMdd_hhmm");
        public Store(IHttpClientFactory httpClientFactory, IConfiguration configuration, IDistributedCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _cache = cache;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var list =  await _cache.GetRecordAsync<IEnumerable<Product>>(recordKey);
            if (list != null) return list;

            list = await GetProductsHttpClient();
            await _cache.SetRecordAsync(recordKey, list);
            return list;
        }

        private async Task<IEnumerable<Product>> GetProductsHttpClient()
        {
            var resp = await _httpClientFactory.CreateClient().GetAsync(GetURL());
            resp.EnsureSuccessStatusCode();

            var list = JsonConvert.DeserializeObject<IEnumerable<Product>>(await resp.Content.ReadAsStringAsync());

            return await Task.FromResult(list);
        }
        private string GetURL() => _configuration["URL_Products"];
    }
}
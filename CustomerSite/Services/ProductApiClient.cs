using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyShop.Share;

namespace CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public ProductApiClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }
        public async Task<IList<ProductVm>> Getproducts()
        {
            var response = await _client.GetAsync($"{_config["Host"]}/api/Product");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<ProductVm> Getproduct(int id)
        {
            var response = await _client.GetAsync($"{_config["Host"]}/api/Product/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IList<ProductVm>> GetProductByCategory(int id)
        {
            var response = await _client.GetAsync($"{_config["Host"]}/api/Product/Category/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

    }
}
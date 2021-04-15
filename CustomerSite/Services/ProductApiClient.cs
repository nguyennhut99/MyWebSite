using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyShop.Share;

namespace CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _client;

        public ProductApiClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<IList<ProductVm>> Getproducts()
        {
            var response = await _client.GetAsync("https://localhost:44358/api/Product");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }

        public async Task<ProductVm> Getproduct(int id)
        {
            var response = await _client.GetAsync($"https://localhost:44358/api/Product/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IList<ProductVm>> GetProductByCategory(int id)
        {
            var response = await _client.GetAsync($"https://localhost:44358/api/Product/Category/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        } 
        
    }
}
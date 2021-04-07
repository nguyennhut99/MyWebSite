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
        
    }
}
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MyShop.Share;


namespace CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public CategoryApiClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var response = await _client.GetAsync($"{_config["Host"]}/api/category");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}
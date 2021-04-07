using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using MyShop.Share;


namespace CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("https://localhost:44358/api/category");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}
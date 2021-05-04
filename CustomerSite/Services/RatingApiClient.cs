using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyShop.Share;


namespace CustomerSite.Services
{
    public class RatingApiClient : IRatingApiClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public RatingApiClient(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }


        public async Task Rating(int productId, int rating)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var ratingCrequest = new RatingCreateRequest{
                ProductId = productId,
                Rating = rating
            };
            var response = await _client.PostAsJsonAsync($"{_config["Host"]}/api/Rating", ratingCrequest);

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsAsync<IList<CartVM>>();

        }
    }
}
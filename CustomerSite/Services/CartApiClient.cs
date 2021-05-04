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
    public class CartApiClient : ICartApiClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public CartApiClient(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public async Task<IList<CartVM>> GetCarts()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync($"{_config["Host"]}/api/Cart");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CartVM>>();
        }

        public async Task<IList<CartVM>> AddProductToCart(int productId, int orderQty)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var CartRequest = new CartCreateRequest
            {
                ProductId = productId,
                OrderQty = orderQty
            };
            var response = await _client.PostAsJsonAsync($"{_config["Host"]}/api/Cart", CartRequest);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CartVM>>();
        }

        public async Task<IList<CartVM>> RemoveCart(int cartId)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.DeleteAsync($"{_config["Host"]}/api/Cart/{cartId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CartVM>>();
        }

        public async Task<IList<CartVM>> UpdateCart(int cartId, int Qty)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.PutAsJsonAsync($"{_config["Host"]}/api/Cart/{cartId}?newOrderQty={Qty}", cartId);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CartVM>>();
        }

        public async Task<IList<CartVM>> Checkout(string Address, string Phone)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);



            var response = await _client.PostAsJsonAsync($"{_config["Host"]}/api/Order", new CheckOutCreateRequest { Address = Address, Phone = Phone });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<IList<CartVM>>();
        }
    }
}
using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.ViewComponents
{
    public class CartBadgesViewComponent : ViewComponent
    {
        private readonly ICartApiClient _cartApiClient;

        public CartBadgesViewComponent(ICartApiClient cartApiClient)
        {
            _cartApiClient = cartApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Carts = await _cartApiClient.GetCarts();

            return View(Carts);
        }
    }
}

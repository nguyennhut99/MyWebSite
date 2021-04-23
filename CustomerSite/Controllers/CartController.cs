using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CustomerSite.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ICartApiClient _cartApiClient;
        
        public CartController(ILogger<CartController> logger, ICartApiClient cartApiClient)
        {
            _logger = logger;
            _cartApiClient = cartApiClient;
        }
        public async Task<IActionResult> CartsView()
        {
            var Carts = await _cartApiClient.GetCarts();     

            return View(Carts);
        }

        public async Task<IActionResult> AddProductToCart(int productId, int orderQty, int? categoryId)
        {
            await _cartApiClient.AddProductToCart(productId, orderQty);
            if (categoryId != null)
            {
                return Redirect($"../Home/Category/{categoryId}");
            }
            return Redirect("../Home/Index");

        }

        [HttpPost] 
        public async Task<IActionResult> AddToCart(IFormCollection form)
        {
            int Id =  Convert.ToInt32(form["id"]);
            int Qty =  Convert.ToInt32(form["quantity"]);
            await _cartApiClient.AddProductToCart(Id, Qty);
            
            return Redirect($"../Product/ProductView/{Id}");

        }

        public async Task<IActionResult> RemoveCart(int cartId)
        {
            await _cartApiClient.RemoveCart(cartId);
            
            return Redirect("../Cart/CartsView");

        }

        
        [HttpPost]        
        public async Task<IActionResult> UpdateCart(IFormCollection form)
        {
            int Id =  Convert.ToInt32(form["id"]);
            int Qty =  Convert.ToInt32(form["quantity"]);
            await _cartApiClient.UpdateCart(Id, Qty);
            
            return Redirect("../Cart/CartsView");

        }

        public async Task<IActionResult> Checkout()
        {
            await _cartApiClient.Checkout();
            
            return Redirect("../Cart/CartsView");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Backend.Data;
using MyShop.Backend.Models;
using MyShop.Backend.Services;
using MyShop.Share;



namespace MyShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserUtility _userUtility;

        public CartController(ApplicationDbContext context, IUserUtility userUtility)
        {
            _context = context;
            _userUtility = userUtility;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CartVM>>> GetCarts()
        {
            return await _context.Carts                
                .Where(c => c.UserID == _userUtility.GetUserId())
                .Include(c => c.product)
                .Select(x => new CartVM
                {
                    Id = x.ProductId,
                    ProductName = x.product.Name,
                    ProductPrice = x.product.Price,
                    ProductDescription = x.product.Description,
                    ThumbnailImageUrl = x.product.ImageFileName
                })
                .ToListAsync();
        }

        // [HttpGet("{id}")]
        // [AllowAnonymous]
        // public async Task<ActionResult<ProductVm>> GetCart(int id)
        // {
        //     var cart = await _context.Carts.FindAsync(id);

        //     if (cart == null)
        //     {
        //         return NotFound();
        //     }

        //     var cartVm = new ProductVm
        //     {
        //         Id = cart.Id,
        //         Name = cart.Name
        //     };

        //     return cartVm;
        // }

        // [HttpPut("{id}")]
        // [Authorize(Roles = "admin")]
        // public async Task<IActionResult> PutCart(int id, CartCreateRequest cartCreateRequest)
        // {
        //     var cart = await _context.Carts.FindAsync(id);

        //     if (cart == null)
        //     {
        //         return NotFound();
        //     }

        //     cart.Name = cartCreateRequest.Name;
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ProductVm>> PostCart(CartCreateRequest cartCreateRequest)
        {
            var cart = new Cart
            {
                UserID = _userUtility.GetUserId(),
                ProductId = cartCreateRequest.ProductId,
                ProductQty = cartCreateRequest.OrderQty
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
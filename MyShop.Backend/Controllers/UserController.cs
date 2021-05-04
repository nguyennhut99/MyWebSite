using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Backend.Data;
using MyShop.Share;



namespace MyShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserVm>>> GetUsers()
        {
            return await _context.Users
                .Select(x => new UserVm
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrders(string id)
        {
            return await _context.OrderHeaders
                .Where(o => o.UserId == id)
                .Select(x => new OrderVm
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    UserId = x.UserId,
                    TotalDue = x.TotalDue
                })
                .ToListAsync();
        }

        [HttpGet("Order/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<OrderDetailVm>>> GetOrder(int id)
        {
            var order = _context.OrderHeaders
                .Where(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = await _context.OrderDetails
            .Where(od => od.OrderId == id)
            .Join(
                _context.Products,
                od => od.ProductId,
                p => p.Id,
                (od, p) => new OrderDetailVm
                {
                    ProductId = od.ProductId,
                    ProductName = p.Name,
                    OrderQty = od.OrderQty,
                    UnitPrice = od.UnitPrice,
                    total = od.UnitPrice * od.OrderQty
                }
            ).ToListAsync();
            return orderDetails;
        }

    }
}
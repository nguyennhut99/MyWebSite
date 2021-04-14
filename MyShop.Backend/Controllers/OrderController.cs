using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Backend.Data;
using MyShop.Backend.Models;
using MyShop.Share;
using MyShop.Backend.Services;



namespace MyShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IUserUtility _userUtility;

        public OrderController(ApplicationDbContext context, IUserUtility userUtility)
        {
            _context = context;
            _userUtility = userUtility;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderVm>>> GetOrders()
        {
            return await _context.OrderHeaders
                .Where(o => o.UserId == _userUtility.GetUserId())
                .Select(x => new OrderVm
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    UserId = x.UserId,
                    TotalDue = x.TotalDue
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<OrderDetailVm>>> GetOrder(int id)
        {
            var order = _context.OrderHeaders
                .Where(o => o.Id == id && o.UserId == _userUtility.GetUserId());                

            if (order == null)
            {
                return NotFound();
            }

            var orderDetails =await _context.OrderDetails
            .Where(od => od.OrderId ==id)
            .Join(
                _context.Products,
                od => od.ProductId,
                p => p.Id,
                (od, p) => new OrderDetailVm{
                    ProductId = od.ProductId,
                    ProductName = p.Name,
                    OrderQty = od.OrderQty,
                    UnitPrice = od.UnitPrice,
                    total = od.UnitPrice * od.OrderQty
                }            
            ).ToListAsync();
            return orderDetails;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<OrderVm>> PostOrder()
        {
            if (_userUtility.GetUserId() == null)
            {
                return NotFound();
            }

            var Carts = await _context.Carts
                .Where(c => c.UserID == _userUtility.GetUserId())
                .Select(x => new Cart{
                    Id = x.Id,
                    ProductId = x.ProductId,
                    UserID = x.UserID,
                    ProductQty = x.ProductQty                    
                })
                .ToListAsync();

            if (Carts.Count == 0)
            {
                return NotFound();
            };

            decimal total = 0;
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (var i in Carts)
            {
                var product = await _context.Products.FindAsync(i.ProductId);
                total += product.Price * i.ProductQty;

                orderDetails.Add(new OrderDetail
                {
                    ProductId = i.ProductId,
                    UnitPrice = product.Price,
                    OrderQty = i.ProductQty
                });
            };


            var order = new OrderHeader
            {
                OrderDate = DateTime.Now,
                UserId = _userUtility.GetUserId(),
                TotalDue = total,

            };

            Carts.ForEach(x => {
                _context.Carts.Remove(x);
            });
            
            _context.OrderHeaders.Add(order);
            await _context.SaveChangesAsync();

            orderDetails.ForEach(orderDetail =>
            {
                orderDetail.OrderId = order.Id;
                _context.OrderDetails.Add(orderDetail);
            });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.OrderHeaders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.OrderHeaders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
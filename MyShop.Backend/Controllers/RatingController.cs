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
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserUtility _userUtility;

        public RatingController(ApplicationDbContext context, IUserUtility userUtility)
        {
            _context = context;
            _userUtility = userUtility;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Rating(RatingCreateRequest ratingCreateRequest)
        {
            var product = await _context.Products.FindAsync(ratingCreateRequest.ProductId);

            if (product == null)
            {
                return NotFound();
            }

            var Rating = await _context.UserRatings
                .Where(r => r.ProductId == ratingCreateRequest.ProductId && r.UserId == _userUtility.GetUserId())
                .ToListAsync();

            if (Rating.Count() == 0)
            {
                _context.UserRatings.Add(new UserRating
                {
                    UserId = _userUtility.GetUserId(),
                    ProductId = ratingCreateRequest.ProductId,
                    Rating = ratingCreateRequest.Rating
                });
                await _context.SaveChangesAsync();
            }
            else
            {
                Rating[0].Rating = ratingCreateRequest.Rating;
                await _context.SaveChangesAsync();
            }

            var ListRating = await _context.UserRatings
                .Where(r => r.ProductId == ratingCreateRequest.ProductId)
                .Select(x => new UserRating
                {
                    Rating = x.Rating
                })
                .ToListAsync();

            var totalRating = 0;

            ListRating.ForEach(x =>
            {
                totalRating += x.Rating;
            });

            product.Rating = totalRating / ListRating.Count();
            product.RatingCount = ListRating.Count();
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
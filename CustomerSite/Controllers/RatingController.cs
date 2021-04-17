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
    public class RatingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRatingApiClient _ratingApiClient;
        public RatingController(ILogger<HomeController> logger, IRatingApiClient ratingApiClient)
        {
            _logger = logger;
            _ratingApiClient = ratingApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> Rating(IFormCollection form)
        {
            int productId =  Convert.ToInt32(form["productId"]);
            int rating =  Convert.ToInt32(form["rating"]);

            await _ratingApiClient.Rating(productId, rating);

            return Redirect($"../Product/ProductView/{productId}");
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

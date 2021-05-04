using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MyShop.Backend.Services
{
    public class UserUtilityRepository : IUserUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserUtilityRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
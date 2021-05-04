using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyShop.Backend.Models
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData]
        public string FullName { get; set; }

        public IList<Cart> Carts { get; private set; } = new List<Cart>();

        public IList<UserRating> UserRatings { get; private set; } = new List<UserRating>();
    }
}
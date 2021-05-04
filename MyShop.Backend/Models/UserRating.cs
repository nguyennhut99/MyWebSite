namespace MyShop.Backend.Models
{
    public class UserRating
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Rating { get; set; }
    }
}
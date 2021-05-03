using System.ComponentModel.DataAnnotations;

namespace MyShop.Share
{
    public class RatingCreateRequest
    {
        [Required]
        public int ProductId { get; set; }

        public int Rating { get; set; }
    }
}
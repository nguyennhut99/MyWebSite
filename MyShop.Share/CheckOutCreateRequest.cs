using System.ComponentModel.DataAnnotations;

namespace MyShop.Share
{
    public class CheckOutCreateRequest
    {
        [Required]
        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
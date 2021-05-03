using System.ComponentModel.DataAnnotations;

namespace MyShop.Share
{
    public class BrandCreateRequest
    {
        [Required]
        public string Name {get; set;}
    }
}
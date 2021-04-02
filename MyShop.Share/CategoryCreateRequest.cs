using System.ComponentModel.DataAnnotations;

namespace MyShop.Share
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name {get; set;}
    }
}
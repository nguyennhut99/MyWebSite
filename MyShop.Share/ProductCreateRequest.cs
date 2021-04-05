using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyShop.Share
{
    public class ProductCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public IFormFile ThumbnailImageUrl { get; set; }

        public int BrandId { get; set; }

    }
}
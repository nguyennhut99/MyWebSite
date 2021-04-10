using System;

namespace MyShop.Share
{
    public class CartVM
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductDescription { get; set; }
        
        public string ThumbnailImageUrl { get; set; }

    }
}
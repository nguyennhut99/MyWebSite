using System;
using System.Collections.Generic;

namespace MyShop.Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageFileName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public decimal Rating { get; set; }

        public int RatingCount { get; set; }

        public IList<UserRating> UserRatings { get; private set; } = new List<UserRating>();

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public IList<ProductCategory> ProductCategories { get; private set; } = new List<ProductCategory>();

        public IList<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();

        public IList<Cart> Carts { get; private set; } = new List<Cart>();
    }
}
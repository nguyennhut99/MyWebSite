using System;
using System.Collections.Generic;

namespace MyShop.Backend.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public String UserId { get; set; }

        public User User { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public decimal TotalDue { get; set; }

        public IList<OrderDetail> OrderDetails { get; private set; } = new List<OrderDetail>();
    }
}
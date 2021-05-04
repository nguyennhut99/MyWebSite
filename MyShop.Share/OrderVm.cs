using System;

namespace MyShop.Share
{
    public class OrderVm
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public String UserId { get; set; }

        public decimal TotalDue { get; set; }
    }
}
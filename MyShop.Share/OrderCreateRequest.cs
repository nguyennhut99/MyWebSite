using System;
using System.Collections.Generic;

namespace MyShop.Share
{
    public class OrderCreateRequest
    {
        public List<OrderDetailCreateRequest> OrderDetails {get; set;}
    }
}
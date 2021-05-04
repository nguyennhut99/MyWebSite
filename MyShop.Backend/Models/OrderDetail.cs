namespace MyShop.Backend.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int OrderQty { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
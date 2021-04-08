namespace MyShop.Share
{
    public class OrderDetailVm
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int OrderQty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal total { get; set; }
    }
}
namespace MyShop.Backend.Models
{
    public class Cart
    {
        public int Id {get; set;}

        public int ProductId {get; set;}

        public Product product {get; set;}

        public string UserID {get; set;}

        public User User {get; set;}

        public int ProductQty {get; set;}
    }
}
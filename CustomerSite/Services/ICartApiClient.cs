using MyShop.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services
{
    public interface ICartApiClient
    {
        Task<IList<CartVM>> GetCarts();
        Task<IList<CartVM>> AddProductToCart(int productId, int orderQty);
        Task<IList<CartVM>> RemoveCart(int cartId);
        Task<IList<CartVM>> UpdateCart(int cartId, int Qty);
    }
}
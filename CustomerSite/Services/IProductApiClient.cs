using System.Collections.Generic;
using System.Threading.Tasks;
using MyShop.Share;

namespace CustomerSite.Services
{
    public interface IProductApiClient
    {
        Task<IList<ProductVm>> Getproducts();
        Task<IList<ProductVm>> GetProductByCategory(int id);

    }
}
using MyShop.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services
{
    public interface IRatingApiClient
    {
        Task Rating(int productId, int rating);
    }
}
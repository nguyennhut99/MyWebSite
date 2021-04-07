using MyShop.Share;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerSite.Services
{
    public interface ICategoryApiClient
    {
        Task<IList<CategoryVm>> GetCategories();
    }
}
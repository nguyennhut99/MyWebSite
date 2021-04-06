using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyShop.Backend.Services
{
    public interface IStorageService
    {
        Task SaveFileAsync(IFormFile FileUpload, string fileName);
        Task DeleteFileAsync(string fileName);
    }
}
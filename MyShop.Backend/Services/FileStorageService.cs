using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MyShop.Backend.Services
{
    public class FileStorageService : IStorageService
    {
        [System.Obsolete]
        private readonly string _userContentFolder;

        [System.Obsolete]
        public FileStorageService(IHostingEnvironment environment)
        {
            _userContentFolder = environment.ContentRootPath + "//wwwroot//images";
        }

        [System.Obsolete]
        public async Task SaveFileAsync(IFormFile FileUpload, string fileName)
        {
            if (FileUpload != null)
            {
                var file = Path.Combine(_userContentFolder, fileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);
                }
            }
        }

        [System.Obsolete]
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }


    }
}
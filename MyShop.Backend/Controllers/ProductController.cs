using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Backend.Data;
using MyShop.Backend.Models;
using MyShop.Backend.Services;
using MyShop.Share;


namespace MyShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;

        public ProductController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProducts()
        {
            return await _context.Products
                .Select(x => new ProductVm
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    rating = x.rating,
                    ThumbnailImageUrl = x.ImageFileName==null?"":Path.Combine("https://localhost:44358/images", x.ImageFileName)
                })
                .ToListAsync();
        }

        [HttpGet("Category/{CategoryId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductVm>>> GetProductByCategory(int CategoryId)
        {
            return await _context.ProductCategory
                .Include(p => p.Product)     
                .Where(p => p.CategoryId == CategoryId)           
                .Select(x => new ProductVm
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    CategoryId = CategoryId,
                    Description = x.Product.Description,
                    rating = x.Product.rating,
                    ThumbnailImageUrl =  x.Product.ImageFileName==null?"":Path.Combine("https://localhost:44358/images", x.Product.ImageFileName)
                                        
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductVm>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productVm = new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                rating = product.rating,
                ratingCount = product.ratingCount,
                ThumbnailImageUrl = Path.Combine("https://localhost:44358/images", product.ImageFileName)
            };

            return productVm;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] ProductCreateRequest productCreateRequest)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productCreateRequest.Name;
            product.Price = productCreateRequest.Price;
            product.Description = productCreateRequest.Description;
            product.BrandId = productCreateRequest.BrandId;

            await _storageService.DeleteFileAsync(product.ImageFileName);
            if (productCreateRequest.ThumbnailImageUrl != null)
            {
                product.ImageFileName = await SaveFile(productCreateRequest.ThumbnailImageUrl);
            }

            _context.ProductCategory.RemoveRange(
                await _context.ProductCategory.Where(i=> i.ProductId.Equals(id))
                .ToListAsync()
            );

            foreach (var Id in productCreateRequest.CategoryId)
            {
                _context.ProductCategory.Add(
                    new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = Id
                    }
                );
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ProductVm>> PostProduct([FromForm] ProductCreateRequest productCreateRequest)
        {

            var product = new Product
            {
                Name = productCreateRequest.Name,
                Price = productCreateRequest.Price,
                Description = productCreateRequest.Description,
                BrandId = productCreateRequest.BrandId
            };

            if (productCreateRequest.ThumbnailImageUrl != null)
            {
                product.ImageFileName = await SaveFile(productCreateRequest.ThumbnailImageUrl);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            foreach (var Id in productCreateRequest.CategoryId)
            {
                _context.ProductCategory.Add(
                    new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = Id
                    }
                );
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _storageService.DeleteFileAsync(product.ImageFileName);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file, fileName);
            return fileName;
        }
    }
}
using System.Threading.Tasks;
using Xunit;
using Moq;
using MyShop.Backend.Controllers;
using MyShop.Share;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyShop.Backend.Models;
using MyShop.Backend.Services;
using Microsoft.Extensions.Configuration;
using System;

namespace MyShop.Backend.Tests
{
    public class ProductControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly ProductController controller;
        private Mock<IStorageService> _storageService;
        private Mock<IConfiguration> _config;

        public ProductControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _storageService = new Mock<IStorageService>();
            _config = new Mock<IConfiguration>();
            controller = new ProductController(_fixture.Context, _storageService.Object, _config.Object);
        }

        [Fact]
        public async Task PostProduct_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test category" });
            await dbContext.SaveChangesAsync();

            var ListId = new List<int>();
            ListId.Add(1);
            var product = new ProductCreateRequest { Name = "Test product", BrandId = 1, CategoryId = ListId };
            var result = await controller.PostProduct(product);

            var createdAtActionResult = Assert.IsType<OkResult>(result.Result);
        }

        [Fact]
        public async Task GetProducts_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            dbContext.Products.Add(new Product { Id = 2, Name = "Test product 2", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var result = await controller.GetProducts();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }

        [Fact]
        public async Task GetProductById_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var result = await controller.GetProduct(1);
            var actionResult = Assert.IsType<ActionResult<ProductVm>>(result);

            Assert.Equal("Test product 1", actionResult.Value.Name);
        }

        [Fact]
        public async Task GetProductById_NotfoundId()
        {
            var dbContext = _fixture.Context;

            var result = await controller.GetProduct(9999);

            var actionResult = Assert.IsType<ActionResult<ProductVm>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PutBrandBy_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test category" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var ListId = new List<int>();
            ListId.Add(1);
            var product = new ProductCreateRequest { Name = "Test put product", BrandId = 1, CategoryId = ListId };
            var result = await controller.PutProduct(1, product);

            var actionResult = Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task PutBrandBy_Fail_IdNotFound()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test category" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var ListId = new List<int>();
            ListId.Add(1);
            var product = new ProductCreateRequest { Name = "Test put product", BrandId = 1, CategoryId = ListId };
            var result = await controller.PutProduct(1999, product);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test category" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var result = await controller.DeleteProduct(1);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Fail_IdNotFound()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            dbContext.Categories.Add(new Category { Id = 1, Name = "Test category" });
            dbContext.Products.Add(new Product { Id = 1, Name = "Test product 1", BrandId = 1 });
            await dbContext.SaveChangesAsync();

            var result = await controller.DeleteProduct(4);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
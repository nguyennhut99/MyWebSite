using System.Threading.Tasks;
using Xunit;
using MyShop.Backend.Controllers;
using MyShop.Share;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyShop.Backend.Models;
using System;

namespace MyShop.Backend.Tests
{
    public class BrandControllerTests : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        private readonly BrandController controller;

        public BrandControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            controller = new BrandController(_fixture.Context);
        }

        [Fact]
        public async Task PostBrand_Success()
        {           
            var brand = new BrandCreateRequest { Name = "Test brand" };            
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);
            Assert.Equal("Test brand", returnValue.Name);
        }

        [Fact]
        public async Task PostBrand_Fail_NameIsNull()
        {
            var brand = new BrandCreateRequest { Name = "" };
            var result = await controller.PostBrand(brand);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetBrand_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var result = await controller.GetBrands();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BrandVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }

        [Fact]
        public async Task GetBrandById_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var result = await controller.GetBrand(1);
            var actionResult = Assert.IsType<ActionResult<BrandVm>>(result);

            Assert.Equal("Test brand", actionResult.Value.Name);
        }

        [Fact]
        public async Task GetBrandById_NotfoundId()
        {
            var dbContext = _fixture.Context;

            var result = await controller.GetBrand(9999);

            var actionResult = Assert.IsType<ActionResult<BrandVm>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PutBrandBy_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var brand = new BrandCreateRequest { Name = "Test put" };
            var result = await controller.PutBrand(1, brand);

            var actionResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutBrandBy_Fail_IdNotFound()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var brand = new BrandCreateRequest { Name = "Test put" };
            var result = await controller.PutBrand(9999, brand);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var result = await controller.DeleteBrand(1);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_Fail_IdNotFound()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Id = 1, Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var result = await controller.DeleteBrand(4);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
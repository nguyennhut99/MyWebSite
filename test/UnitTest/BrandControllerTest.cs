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

        public BrandControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task PostBrand_Success()
        {
            var dbContext = _fixture.Context;
            var brand = new BrandCreateRequest { Name = "Test brand" };

            var controller = new BrandController(dbContext);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);
            Assert.Equal("Test brand", returnValue.Name);
        }

        [Fact]
        public async Task GetBrand_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Brands.Add(new Brand { Name = "Test brand" });
            await dbContext.SaveChangesAsync();

            var controller = new BrandController(dbContext);
            var result = await controller.GetBrands();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BrandVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }

        [Fact]
        public async Task GetBrandById_Success()
        {
            var dbContext = _fixture.Context;
            var brand = new BrandCreateRequest { Name = "Test brand" };

            var controller = new BrandController(dbContext);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);

            var result2 = await controller.GetBrand(returnValue.Id);
            var actionResult = Assert.IsType<ActionResult<BrandVm>>(result2);

            Assert.Equal("Test brand", actionResult.Value.Name);
        }

        [Fact]
        public async Task delete_Success()
        {
            var dbContext = _fixture.Context;
            var brand = new BrandCreateRequest { Name = "Test brand" };

            var controller = new BrandController(dbContext);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);

            var result2 = await controller.DeleteBrand(returnValue.Id);
            // assert
            Assert.IsType<OkResult>(result2);
            // Assert.Equal(404, result2.StatusCode);
        }
    }
}
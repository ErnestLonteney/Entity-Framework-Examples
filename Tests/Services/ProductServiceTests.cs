using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Services;
using Services.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Tests.Services
{
    [ExcludeFromCodeCoverage]
    public class ProductServiceTests
    {
        private readonly IMapper mapper;

        public ProductServiceTests()
        {                      
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                 cfg.AddProfile<MappingConfig>();
            }, NullLoggerFactory.Instance);
            
            this.mapper = mapperConfig.CreateMapper();        
        }


        [Fact]
        public void GetAllProducts_CorrectInput_ListOfProducts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using var context = new ShopDbContext(options);

            context.Products.Add(new Product { Name = "Product1", Price = 10.0m });
            context.Products.Add(new Product { Name = "Product2", Price = 20.0m });
            context.SaveChanges();

            var productService = new ProductService(context, mapper);

            // Act
            var products = productService.GetAllProducts();
         
            // Assert
          Assert.NotNull(products);
          Assert.Equal(2, products.Count());         
        }
    }
}

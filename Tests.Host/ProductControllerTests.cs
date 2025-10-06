using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MyShop.Configuration;
using MyShop.Controllers;
using Services.Interfaces;
using Services.Models;

namespace Tests.Host
{
    public class ProductControllerTests
    {
        private readonly ProductController controller;
        private readonly Mock<IProductService> productServiceMock;
        private readonly IMapper mapper;

        public ProductControllerTests()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            }, NullLoggerFactory.Instance).CreateMapper();

            productServiceMock = new Mock<IProductService>();   
            controller = new ProductController(productServiceMock.Object, mapper);  
        }


        #region Index

        [Fact]
        public void Index_CorrectInput_OkResult()
        {
            // Arrange 
            productServiceMock.Setup(x => x.GetAllProducts())
                .Returns(new List<ProductModel>
                {
                    new ProductModel { Name = "Product 1", Price = 10.0m },
                    new ProductModel { Name = "Product 2", Price = 20.0m }
                });

            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        #endregion
    }
}

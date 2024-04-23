using AutoMapper;
using InventoryManagerAPI.Controllers;
using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using InventoryManagerDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace InventoryManagerServiceTests
{
    [TestFixture]
    internal class ProductServiceTests
    {
        private ProductController _controller;
        private Mock<IProductService> _productServiceMock;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductRequest, Product>();
            });

            _productServiceMock = new Mock<IProductService>();

            _controller = new ProductController(_productServiceMock.Object);
        }

        [Test]
        public void CreateProductBoundryValueAnalysis()
        {
            //Clase de echivalenta:
            //Domeniul de intrari:
            //name length < 3
            //name length >= 3 =< 50
            //name length > 50
            //description length < 3
            //description length >= 3 =< 50
            //description length > 50
            //stock < 1
            //stock >= 1 =< double.MaxValue
            //stock > double.MaxValue
            //price < 1
            //price >= 1 =< dobule.MaxValue
            //price > double.MaxValue
            //discount < 0
            //discount >= 0 =< 100
            //discount > 100

            //Domeniul de iesiri:
            //produs creat => CreatedAtActionResult status code 201
            //produs invalid => BadRequestObjectResult status code 400

            string nameGoodValue = "TestProduct";
            string descriptionGoodValue = "TestDescription";
            int stockGoodValue = 1;
            float priceGoodValue = 1;
            int discountGoodValue = 1;
            int categoryGoodValue = 1;

            ProductRequest product = new ProductRequest
            {
                Name = nameGoodValue,
                Description = descriptionGoodValue,
                Stock = stockGoodValue,
                Price = priceGoodValue,
                Discount = discountGoodValue,
                CategoryId = categoryGoodValue
            };

            var goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            // Name cases

            //name null
            product = new ProductRequest
            {
                Description = descriptionGoodValue,
                Stock = stockGoodValue,
                Price = priceGoodValue,
                Discount = discountGoodValue,
                CategoryId = categoryGoodValue
            };
            var badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //name < 3
            product.Name = "12";
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //name = 3
            product.Name = "123";
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //name = 50
            product.Name = "Lorem Lorem ipsum dolor sit amet, consectetur adip";
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //name = 51
            product.Name = "Lorem Lorem ipsum dolor sit amet, consectetur adipi";
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            // Description cases

            //description null
            product = new ProductRequest
            {
                Name = nameGoodValue,
                Stock = stockGoodValue,
                Price = priceGoodValue,
                Discount = discountGoodValue,
                CategoryId = categoryGoodValue
            };
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //description < 3
            product.Description = "12";
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //description = 3
            product.Description = "123";
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //description = 100
            product.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ";
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //description = 101
            product.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore 1";
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            product.Description = descriptionGoodValue;

            // Stock cases

            //stock = 0
            product.Stock = 0;
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //stock = 1
            product.Stock = 1;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //stock = int Max
            product.Stock = int.MaxValue;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            product.Stock = stockGoodValue;

            // Price cases

            //price = 0.99
            product.Price = 0.99;
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //price = 1
            product.Price = 1;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //price = double max
            product.Price = double.MaxValue;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            product.Price = priceGoodValue;

            // Discount cases

            //discount = -1
            product.Discount = -1;
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);

            //discount = 0
            product.Discount = 0;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //discount = 100
            product.Discount = 100;
            goodResult = _controller.Post(product) as CreatedAtActionResult;
            Assert.AreEqual(201, goodResult.StatusCode);

            //discount = 101
            product.Discount = 101;
            badResult = _controller.Post(product) as BadRequestObjectResult;
            Assert.AreEqual(400, badResult.StatusCode);
        }
    }
}

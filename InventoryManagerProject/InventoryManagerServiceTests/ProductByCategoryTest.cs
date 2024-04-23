using AutoMapper;
using InventoryManagerAPI.Controllers;
using InventoryManagerBusiness.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagerAPI.Controllers;
using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using InventoryManagerBusiness.Services;
using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace InventoryManagerServiceTests
{
    [TestFixture]
    internal class ProductByCategoryTest
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
        public void GetProductByCategory()
        {

        }
    }
}

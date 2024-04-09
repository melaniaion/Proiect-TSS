using AutoMapper;
using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Services;
using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace InventoryManagerServiceTests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private IMapper _mapper;
        private CategoryService _categoryService;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryRequest, Category>();
            });
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _mapper = config.CreateMapper();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapper);
        }

        [Test]
        public void CreateCategory()
        {
            // Arrange
            var categoryRequest = new CategoryRequest { Name = "Test Category", Description = "Test Description" };
            _categoryRepositoryMock.Setup(repo => repo.Create(It.IsAny<Category>())).Returns(1);

            // Act
            var result = _categoryService.Create(categoryRequest);

            // Assert
            _categoryRepositoryMock.Verify(repo => repo.Create(It.IsAny<Category>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }
    }
}
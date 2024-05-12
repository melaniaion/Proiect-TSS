using AutoMapper;
using Moq;
using NUnit.Framework;
using InventoryManagerDataAccess.Entities;
using InventoryManagerBusiness.Services;
using InventoryManagerDataAccess.Interfaces;
using InventoryManagerBusiness.DTOs;
using Assert = NUnit.Framework.Assert;

namespace InventoryManagerServiceTests
{
    [TestFixture]
    internal class ProductByCategoryTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<ICategoryRepository> _mockCategoryRepository;
        private Mock<IMapper> _mockMapper;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();

            _productService = new ProductService(_mockProductRepository.Object, _mockCategoryRepository.Object, _mockMapper.Object);
        }

        //Acoperire de Condiție
        [Test]
        public void GetByCategory_CategoryNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            int categoryId = 1;
            int index = 0;
            _mockCategoryRepository.Setup(repo => repo.Get(categoryId)).Returns((Category)null);

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex.Message, Is.EqualTo($"The category with the specified ID ({categoryId}) was not found or the index value was less than 0."));
        }

        //Acoperire de Condiție
        [Test]
        public void GetByCategory_NegativeIndex_ThrowsKeyNotFoundException()
        {
            // Arrange
            int categoryId = 1;
            int index = -1;
            _mockCategoryRepository.Setup(repo => repo.Get(categoryId)).Returns(new Category());

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex.Message, Is.EqualTo($"The category with the specified ID ({categoryId}) was not found or the index value was less than 0."));
        }

        //Acoperire de Decizie
        [Test]
        public void GetByCategory_NoProductsInCategory_ThrowsKeyNotFoundException()
        {
            // Arrange
            int categoryId = 1;
            int index = 0;
            _mockCategoryRepository.Setup(repo => repo.Get(categoryId)).Returns(new Category());
            _mockProductRepository.Setup(repo => repo.GetByCategory(categoryId)).Returns(new List<Product>());

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex.Message, Is.EqualTo($"No products were found for the specified category ID ({categoryId})."));
        }

        //Acoperire de Decizie
        [Test]
        public void GetByCategory_IndexGreaterThanProductCount_ThrowsKeyNotFoundException()
        {
            // Arrange
            int categoryId = 1;
            int index = 3;
            var products = new List<Product> { new Product(), new Product() }; // Only 2 products
            _mockCategoryRepository.Setup(repo => repo.Get(categoryId)).Returns(new Category());
            _mockProductRepository.Setup(repo => repo.GetByCategory(categoryId)).Returns(products);
            _mockMapper.Setup(m => m.Map<List<ProductResponse>>(It.IsAny<List<Product>>())).Returns(new List<ProductResponse> { new ProductResponse(), new ProductResponse() });

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex.Message, Is.EqualTo($"There are not ({index}) products in this category ({categoryId})."));
        }

        //Test pentru scenariul de succes - Acoperire de Instrucțiune
        [Test]
        public void GetByCategory_Success_ReturnsCorrectProductsAndDiscounts()
        {
            int categoryId = 1;
            int index = 2;
            
            //first if
            
            Category category = null;
            List<Product> products = new();
            List<ProductResponse> productsMapped = new();
            List<ProductResponse> productResponses = new();

            _mockCategoryRepository.Setup(x => x.Get(categoryId)).Returns(category);
            _mockProductRepository.Setup(x => x.GetByCategory(categoryId)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<ProductResponse>>(products)).Returns(productsMapped);

            var ex1 = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex1.Message, Is.EqualTo($"The category with the specified ID ({categoryId}) was not found or the index value was less than 0."));

            //second if

            category = new Category { Id = categoryId, Name = "Category 1", Description = "Desc 1" };
            _mockCategoryRepository.Setup(x => x.Get(categoryId)).Returns(category);

            var ex2 = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex2.Message, Is.EqualTo($"No products were found for the specified category ID ({categoryId})."));

            //third if

            products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100, Stock = 10, Discount = 10, Description = "Desc 1", CategoryId = categoryId },
                new Product { Id = 2, Name = "Product 2", Price = 200, Stock = 20, Discount = 20, Description = "Desc 2", CategoryId = categoryId },
                new Product { Id = 3, Name = "Product 3", Price = 300, Stock = 30, Discount = 30, Description = "Desc 3", CategoryId = categoryId }
            };
            _mockProductRepository.Setup(x => x.GetByCategory(categoryId)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<ProductResponse>>(products)).Returns(productsMapped);

            var ex3 = Assert.Throws<KeyNotFoundException>(() => _productService.GetByCategory(categoryId, index));
            Assert.That(ex3.Message, Is.EqualTo($"There are not ({index}) products in this category ({categoryId})."));

            //else

            productsMapped = new List<ProductResponse>
            {
                new ProductResponse { Id = 1, Name = "Product 1", FullPrice = 100, Stock = 10, Discount = 10, Description = "Desc 1", CategoryId = categoryId, DiscountedPrice = 90 },
                new ProductResponse { Id = 2, Name = "Product 2", FullPrice = 200, Stock = 20, Discount = 20, Description = "Desc 2", CategoryId = categoryId, DiscountedPrice = 160 },
                new ProductResponse { Id = 3, Name = "Product 3", FullPrice = 300, Stock = 30, Discount = 30, Description = "Desc 3", CategoryId = categoryId, DiscountedPrice = 210 }
            };
            productResponses = new List<ProductResponse>
            {
                new ProductResponse { Id = 1, Name = "Product 1", FullPrice = 100, Discount = 10, Stock = 10, Description = "Desc 1", CategoryId = categoryId, DiscountedPrice = 90 },
                new ProductResponse { Id = 2, Name = "Product 2", FullPrice = 200, Discount = 20, Stock = 20, Description = "Desc 2", CategoryId = categoryId, DiscountedPrice = 160 }
            };
            _mockMapper.Setup(x => x.Map<List<ProductResponse>>(products)).Returns(productsMapped);

            var result = _productService.GetByCategory(categoryId, index);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(index), "The number of returned products should match the index provided.");
            for (int i = 0; i < productResponses.Count; i++)
            {
                var expectedDiscountedPrice = productResponses[i].FullPrice - (productResponses[i].FullPrice * productResponses[i].Discount / 100);
                Assert.That(result[i].DiscountedPrice, Is.EqualTo(expectedDiscountedPrice), "The discounted price should be calculated correctly.");
            }
        }

        [Test]
        public void GetByCategory_Mutants()
        {
            int categoryId = 1;
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100, Stock = 10, Discount = 10, Description = "Desc 1", CategoryId = categoryId },
                new Product { Id = 2, Name = "Product 2", Price = 200, Stock = 20, Discount = 20, Description = "Desc 2", CategoryId = categoryId },
                new Product { Id = 3, Name = "Product 3", Price = 300, Stock = 30, Discount = 30, Description = "Desc 3", CategoryId = categoryId }
            };
            var productsMapped = new List<ProductResponse>
            {
                new ProductResponse { Id = 1, Name = "Product 1", FullPrice = 100, Stock = 10, Discount = 10, Description = "Desc 1", CategoryId = categoryId, DiscountedPrice = 90 },
                new ProductResponse { Id = 2, Name = "Product 2", FullPrice = 200, Stock = 20, Discount = 20, Description = "Desc 2", CategoryId = categoryId, DiscountedPrice = 160 },
                new ProductResponse { Id = 3, Name = "Product 3", FullPrice = 300, Stock = 30, Discount = 30, Description = "Desc 3", CategoryId = categoryId, DiscountedPrice = 210 }
            };
            List<ProductResponse>[] productResponsesArray = new List<ProductResponse>[4];

            _mockCategoryRepository.Setup(x => x.Get(categoryId)).Returns(new Category { Id = categoryId, Name = "Category 1", Description = "Desc 1" });
            _mockProductRepository.Setup(x => x.GetByCategory(categoryId)).Returns(products);
            _mockMapper.Setup(x => x.Map<List<ProductResponse>>(products)).Returns(productsMapped);

            //program
            for(int index = 0; index <= 3; index++)
            {
                var result = _productService.GetByCategory(categoryId, index);
                productResponsesArray[index] = result;
            }

            //mutant 1
            List<ProductResponse>[] mutant1ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant1Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM1(categoryId, index);
                    mutant1ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant1Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant1Distinct = true;
                    break;
                }
            }

            //mutant 2
            List<ProductResponse>[] mutant2ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant2Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM2(categoryId, index);
                    mutant2ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant2Distinct = true;
                        break;
                    }
                }
                catch(KeyNotFoundException err)
                {
                    mutant2Distinct = true;
                    break;
                }
            }

            //mutant 3
            List<ProductResponse>[] mutant3ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant3Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM3(categoryId, index);
                    mutant3ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant3Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant3Distinct = true;
                    break;
                }
            }

            //mutant 4
            List<ProductResponse>[] mutant4ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant4Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM4(categoryId, index);
                    mutant4ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant4Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant4Distinct = true;
                    break;
                }
            }

            //mutant 5
            List<ProductResponse>[] mutant5ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant5Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM5(categoryId, index);
                    mutant5ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant5Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant5Distinct = true;
                    break;
                }
            }

            //mutant 6
            List<ProductResponse>[] mutant6ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant6Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM6(categoryId, index);
                    mutant6ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant6Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant6Distinct = true;
                    break;
                }
            }

            //mutant 7
            List<ProductResponse>[] mutant7ResponsesArray = new List<ProductResponse>[4];
            Boolean mutant7Distinct = false;
            for (int index = 0; index <= 3; index++)
            {
                try
                {
                    var result = _productService.GetByCategoryM7(categoryId, index);
                    mutant7ResponsesArray[index] = result;
                    if (!result.SequenceEqual(productResponsesArray[index]))
                    {
                        mutant7Distinct = true;
                        break;
                    }
                }
                catch (KeyNotFoundException err)
                {
                    mutant7Distinct = true;
                    break;
                }
            }
        }
    }
}

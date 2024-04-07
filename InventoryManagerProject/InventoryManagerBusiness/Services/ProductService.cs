﻿using AutoMapper;
using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;

namespace InventoryManagerBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public int Create(ProductRequest productDto)
        {
            Product newProduct = _mapper.Map<Product>(productDto);
            Product existingtProduct = _productRepository.Get(newProduct.Id);
            if (existingtProduct != null)
            {
                throw new InvalidOperationException("A product with the same ID already exists.");
            }
            int newProductId = _productRepository.Create(newProduct);
            return newProductId;
        }

        public List<ProductResponse> Get()
        {
            List<Product> products = _productRepository.GetAll();
            List<ProductResponse> productsDto = _mapper.Map<List<ProductResponse>>(products);
            return productsDto;
        }

        public ProductResponse Get(int id)
        {
            Product product = _productRepository.Get(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }

            ProductResponse productDto = _mapper.Map<ProductResponse>(product);
            return productDto;
        }

        public List<ProductResponse> GetByCategory(int categoryId)
        {
            List<Product> products = _productRepository.GetByCategory(categoryId);
            if (products.Count == 0)
            {
                throw new KeyNotFoundException($"No products were found for the specified category ID ({categoryId}).");
            }
            List<ProductResponse> productsDto = _mapper.Map<List<ProductResponse>>(products);
            return productsDto;
        }

        public void Update(int id, ProductRequest updatedProductDto)
        {
            Product productToUpdate = _productRepository.Get(id);
            if (productToUpdate == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }

            Product updatedProduct = _mapper.Map<Product>(updatedProductDto);
            _productRepository.Update(updatedProduct, productToUpdate);
        }

        public void Delete(int id)
        {
            Product product = _productRepository.Get(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }
            _productRepository.Delete(product);
        }
    }
}
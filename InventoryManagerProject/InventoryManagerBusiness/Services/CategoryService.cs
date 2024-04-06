using AutoMapper;
using InventoryManagerBusiness.DTOs;
using InventoryManagerBusiness.Interfaces;
using InventoryManagerDataAccess.Entities;
using InventoryManagerDataAccess.Interfaces;

namespace InventoryManagerBusiness.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository productRepository, IMapper mapper)
        {
            _categoryRepository = productRepository;
            _mapper = mapper;
        }

        public int Create(CategoryDTO categoryDTO)
        {
            Category newCategory = _mapper.Map<Category>(categoryDTO);
            Category existingCategory = _categoryRepository.Get(newCategory.Id);
            if (existingCategory != null)
            {
                throw new InvalidOperationException("A category with the same ID already exists.");
            }
            int newCategoryId = _categoryRepository.Create(newCategory);
            return newCategoryId;
        }

        public List<CategoryDTO> Get()
        {
            List<Category> categories = _categoryRepository.GetAll();
            List<CategoryDTO> categoryDTO = _mapper.Map<List<CategoryDTO>>(categories);
            return categoryDTO;
        }

        public CategoryDTO Get(int id)
        {
            Category category = _categoryRepository.Get(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }

            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public void Update(int id, CategoryDTO updatedCategoryDTO)
        {
            Category category = _categoryRepository.Get(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }

            Category updatedCategory = _mapper.Map<Category>(updatedCategoryDTO);
            _categoryRepository.Update(id, updatedCategory, category);
        }

        public void Delete(int id)
        {
            Category category = _categoryRepository.Get(id);
            if(category == null)
            {
                throw new KeyNotFoundException($"The product with the specified ID ({id}) was not found.");
            }

            _categoryRepository.Delete(id, category);
        }
    }
}

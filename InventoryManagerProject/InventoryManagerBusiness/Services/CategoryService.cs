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
            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public void Update(int id, CategoryDTO updatedCategoryDTO)
        {
            Category category = _mapper.Map<Category>(updatedCategoryDTO);
            _categoryRepository.Update(id, category);
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }
    }
}

using CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist;
using CoinMapWebAPI.BLL.Services.Exceptions.NotFound;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategoryAsync(string name, string creatorId)
        {
            var category = await _categoryRepository.FindByCategoryNameAsync(name);

            if (category != null)
                throw new CategoryAlreadyExistException(name);

            var categoryToAdd = new Category()
            {
                CategoryName = name,
                CreatorId = creatorId
            };

             await _categoryRepository.AddAsync(categoryToAdd);

            return categoryToAdd;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
              throw new CategoryNotFoundException(id);

            return category;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task EditCategoryAsync(string categoryName, int categoryId)
        {
            var categoryToEdit = await _categoryRepository.GetByIdAsync(categoryId);

            if (categoryToEdit == null)
                throw new CategoryNotFoundException(categoryId);

            var categoryByName = await _categoryRepository.FindByCategoryNameAsync(categoryName);
            if (categoryByName != null)
                throw new CategoryAlreadyExistException(categoryName);

            categoryToEdit.CategoryName = categoryName;
            categoryToEdit.ModificationDate = DateTime.Now;

            await _categoryRepository.EditAsync(categoryToEdit);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
                throw new CategoryNotFoundException(categoryId);

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<Venue>> GetAllVenuesFromCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
                throw new CategoryNotFoundException(categoryId);

            return await _categoryRepository.GetAllVenuesFromCategoryAsync(categoryId);
        }
    }
}

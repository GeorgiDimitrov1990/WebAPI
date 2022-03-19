using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Intefaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(string name, string creatorId);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task EditCategoryAsync(string categoryName, int categoryId);
        Task DeleteCategoryAsync(int categoryId);
    }
}

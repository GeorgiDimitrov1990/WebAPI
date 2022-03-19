using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByCategoryNameAsync(string categoryName);
        Task<List<Venue>> GetAllVenuesFromCategoryAsync(int id);
    }
}

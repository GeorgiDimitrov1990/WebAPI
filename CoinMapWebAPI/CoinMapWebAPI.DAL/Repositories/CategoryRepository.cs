using CoinMapWebAPI.DAL.Data;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
            
        }

        public async Task<Category> FindByCategoryNameAsync(string categoryName)
        {
            return await _context.Categories.FirstOrDefaultAsync(u => u.CategoryName.Equals(categoryName));
        }

        public async Task<List<Venue>> GetAllVenuesFromCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            return category.Venues.ToList();
        }
    }
}

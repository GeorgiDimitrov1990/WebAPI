using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

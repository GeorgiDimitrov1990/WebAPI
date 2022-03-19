using CoinMapWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services.Intefaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string username, string password, string firstName, string lastName, string email, string role);
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetCurrentUserAsync(ClaimsPrincipal user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task EditUserAsync(string userId, string username, string firstName, string lastName);
        Task ChangeUserEmailAsync(string userId, string newEmail);
        Task ChangeUserPasswordAsync(string userId, string newPassword);
        Task ChangeUserRoleAsync(string userId, string currentRole, string newRole);
        Task DeleteUserAsync(string userId, string currentUserId);
        Task<bool> IsUserInRoleAsync(string userId, string role);
    }
}

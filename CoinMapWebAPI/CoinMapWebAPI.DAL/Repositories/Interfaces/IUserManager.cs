using CoinMapWebAPI.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories.Interfaces
{
    public interface IUserManager
    {
		Task<List<User>> GetAllAsync();
		Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);
		Task<IdentityResult> CreateUserAsync(User user, string password);
		Task<IdentityResult> EditUserAsync(User user);
		Task<IdentityResult> DeleteUserAsync(User user);
		Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);
		Task<string> GeneratePasswordResetTokenAsync(User user);
		Task<IdentityResult> ConfirmEmailAsync(User user, string token);
		Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token);
		Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
		Task<List<string>> GetUserRolesAsync(User user);
		Task<bool> IsUserInRoleAsync(string userId, string roleName);
		Task<IdentityResult> AddUserToRoleAsync(User user, string role);
		Task<IdentityResult> RemoveUserFromRoleAsync(User user, string role);
		Task<User> FindByIdAsync(string id);
		Task<User> FindByUserNameAsync(string userName);
		Task<User> FindByEmailAsync(string email);
		Task<bool> ValidateUserCredentialsAsync(string userName, string password);
	}
}

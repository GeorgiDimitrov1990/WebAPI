using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.DAL.Repositories
{
    public class AuthUserManager : UserManager<User>, IUserManager
    {
		public AuthUserManager(IUserStore<User> store,
			IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<User> passwordHasher,
			IEnumerable<IUserValidator<User>> userValidators,
			IEnumerable<IPasswordValidator<User>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			IServiceProvider services,
			ILogger<UserManager<User>> logger) :
			base(store,
			optionsAccessor,
			passwordHasher,
			userValidators,
			passwordValidators,
			keyNormalizer,
			errors,
			services,
			logger)
		{
		}

		public async Task<IdentityResult> CreateUserAsync(User user, string password)
		{
			return await CreateAsync(user, password);
		}

		public async Task<User> FindByUserNameAsync(string userName)
		{
			return await FindByNameAsync(userName);
		}

		public async Task<List<string>> GetUserRolesAsync(User user)
		{
			return (await GetRolesAsync(user)).ToList();
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await Users.ToListAsync();
		}

		public async Task<IdentityResult> EditUserAsync(User user)
		{
			return await UpdateAsync(user);
		}

		public async Task<IdentityResult> ChangeUserPasswordAsync(User user, string currentPassword, string newPassword)
		{
			return await ChangePasswordAsync(user, currentPassword, newPassword);
		}

		public async Task<IdentityResult> DeleteUserAsync(User user)
		{
			return await DeleteAsync(user);
		}

		public async Task<IdentityResult> AddUserToRoleAsync(User user, string role)
		{
			return await AddToRoleAsync(user, role);
		}

		public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
		{
			User user = await FindByIdAsync(userId);
			return await IsInRoleAsync(user, roleName);
		}

		public async Task<IdentityResult> RemoveUserFromRoleAsync(User user, string role)
		{
			return await RemoveFromRoleAsync(user, role);
		}

		public async Task<bool> ValidateUserCredentialsAsync(string userName, string password)
		{
			User user = await FindByNameAsync(userName);
			if (user != null)
			{
				bool result = await CheckPasswordAsync(user, password);
				return result;
			}
			return false;
		}
	}
}

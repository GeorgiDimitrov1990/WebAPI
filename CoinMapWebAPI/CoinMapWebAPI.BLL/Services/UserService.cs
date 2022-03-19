using CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist;
using CoinMapWebAPI.BLL.Services.Exceptions.BadRequest;
using CoinMapWebAPI.BLL.Services.Exceptions.NotFound;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoinMapWebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> CreateUserAsync(string username, string password, string firstName, string lastName, string email, string role)
        {
            if (await _userManager.FindByUserNameAsync(username) != null)
                throw new UserNameAlreadyExistsException(username);

            if (await _userManager.FindByEmailAsync(email) != null)
                throw new UserEmailAlreadyExistsException(email);

            User newUser = new User()
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            await _userManager.CreateUserAsync(newUser, password);

            switch (role)
            {
                case "Admin":
                    await _userManager.AddUserToRoleAsync(newUser, "Admin");
                    break;
                case "Regular":
                default:
                    await _userManager.AddUserToRoleAsync(newUser, "Regular");
                    break;
            }

            return newUser;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId);

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _userManager.FindByUserNameAsync(username);

            if (user == null)
                throw new UserNameNotFoundException(username);

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UserEmailNotFoundException(email);

            return user;
        }

        public async Task<User> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.GetAllAsync();
        }

        public async Task EditUserAsync(string userId, string username, string firstName, string lastName)
        {
            var dbUser = await _userManager.FindByIdAsync(userId);

            if (dbUser == null)
                throw new UserNotFoundException(userId);

            if (await _userManager.FindByUserNameAsync(username) != null)
                throw new UserNameAlreadyExistsException(username);

            if (dbUser.UserName == "admin")
                throw new IncorrectAdminModificationException();

            dbUser.UserName = string.IsNullOrEmpty(username) ? dbUser.UserName : username;
            dbUser.FirstName = string.IsNullOrEmpty(firstName) ? dbUser.FirstName : firstName;
            dbUser.LastName = string.IsNullOrEmpty(lastName) ? dbUser.LastName : lastName;

            await _userManager.EditUserAsync(dbUser);
        }

        public async Task ChangeUserEmailAsync(string userId, string newEmail)
        {
            var dbUser = await _userManager.FindByIdAsync(userId);

            if (dbUser == null)
                throw new UserNotFoundException(userId);

            if (await _userManager.FindByEmailAsync(newEmail) != null)
                throw new UserEmailAlreadyExistsException(newEmail);

            if (dbUser.UserName == "admin")
                throw new IncorrectAdminModificationException();

            var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(dbUser, newEmail);

            await _userManager.ChangeEmailAsync(dbUser, newEmail, changeEmailToken);
        }

        public async Task ChangeUserPasswordAsync(string userId, string newPassword)
        {
            var dbUser = await _userManager.FindByIdAsync(userId);

            if (dbUser == null)
                throw new UserNotFoundException(userId);

            if (dbUser.UserName == "admin")
                throw new IncorrectAdminModificationException();

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(dbUser);
            var identityResult = await _userManager.ResetPasswordAsync(dbUser, resetPasswordToken, newPassword);

            if (!identityResult.Succeeded)
                throw new IncorrectPasswordException(identityResult.Errors.FirstOrDefault().Description);
        }

        public async Task ChangeUserRoleAsync(string userId, string currentRole, string newRole)
        {
            var dbUser = await _userManager.FindByIdAsync(userId);

            if (dbUser == null)
                throw new UserNotFoundException(userId);

            if (!await _userManager.IsUserInRoleAsync(userId, currentRole))
                throw new UserInRoleNotFoundException(userId, currentRole);

            if (currentRole == newRole)
                throw new UserIsAlreadyInRoleException(userId, newRole);

            if (dbUser.UserName == "admin")
                throw new IncorrectAdminModificationException();

            await _userManager.RemoveUserFromRoleAsync(dbUser, currentRole);
            await _userManager.AddUserToRoleAsync(dbUser, newRole);
        }

        public async Task DeleteUserAsync(string userId, string currentUserId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId);

            if (user.UserName == "admin")
                throw new IncorrectAdminModificationException();

            if (userId == currentUserId)
                throw new IncorrectDeletionException();

            await _userManager.DeleteUserAsync(user);
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            return await _userManager.IsUserInRoleAsync(userId, role);
        }
    }
}

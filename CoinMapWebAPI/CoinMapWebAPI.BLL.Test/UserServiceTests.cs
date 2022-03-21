using CoinMapWebAPI.BLL.Services;
using CoinMapWebAPI.BLL.Services.Exceptions.AlreadyExist;
using CoinMapWebAPI.BLL.Services.Exceptions.BadRequest;
using CoinMapWebAPI.BLL.Services.Exceptions.NotFound;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Xunit;

namespace CoinMapWebAPI.BLL.Test
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly Mock<IUserManager> _userManagerStub = new Mock<IUserManager>();

        private readonly string testUserId = Guid.NewGuid().ToString("D");
        private readonly string testAdminId = Guid.NewGuid().ToString("D");
        private readonly string invalidUserId = "Invalid User Id";

        private const string TestUsername = "fake_username";
        private const string TestPassword = "fake_password";
        private const string TestFirstName = "Fake First Name";
        private const string TestLastName = "Fake Last Name";
        private const string TestEmail = "test-email@fake.com";
        private const string NewTestEmail = "new-email@fake.com";
        private const string TestEmailToken = "Test Email Token";
        private const string InvalidRole = "Invalid";

        public UserServiceTests()
        {
            _sut = new UserService(_userManagerStub.Object);
        }

        #region CreateUser Test Methods
        [Fact]
        public async Task CreateUser_ValidData_ShouldVerifyAndReturnCreatedUser()
        {

            // act
            var createdUser = await _sut.CreateUserAsync(TestUsername, TestPassword, TestFirstName, 
                TestLastName, TestEmail, "Regular");

            // assert
            Assert.NotNull(createdUser);
            Assert.IsType<User>(createdUser);
            Assert.Equal(TestUsername, createdUser.UserName);
            Assert.Equal(TestFirstName, createdUser.FirstName);
            Assert.Equal(TestLastName, createdUser.LastName);
            Assert.Equal(TestEmail, createdUser.Email);
        }

        [Fact]
        public async Task CreateUser_AlreadyExistingUsername_ShouldThrowUserNameAlreadyExistsException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByUserNameAsync(TestUsername)).ReturnsAsync(new User());

            // act

            // assert
            await Assert.ThrowsAsync<UserNameAlreadyExistsException>(() => _sut.CreateUserAsync(TestUsername,
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task CreateUser_AlreadyExistingEmail_ShouldThrowUserEmailAlreadyExistsException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(new User());

            // act

            // assert
            await Assert.ThrowsAsync<UserEmailAlreadyExistsException>(() => _sut.CreateUserAsync(It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), TestEmail, It.IsAny<string>()));
        }

        [Fact]
        public async Task CreateUser_ValidAdminRole_ShouldSetUserRoleToAdmin()
        {

            // act
            var createdUser = await _sut.CreateUserAsync(TestUsername, TestPassword, TestFirstName, 
                TestLastName, TestEmail, "Admin");

            // assert
            _userManagerStub.Verify(um => um.AddUserToRoleAsync(createdUser, "Admin"), Times.Once);
        }

        [Fact]
        public async Task CreateUser_ValidRegularRole_ShouldSetUserRoleToRegular()
        {

            // act
            var createdUser = await _sut.CreateUserAsync(TestUsername, TestPassword, TestFirstName, 
                TestLastName, TestEmail, "Regular");

            // assert
            _userManagerStub.Verify(um => um.AddUserToRoleAsync(createdUser, "Regular"), Times.Once);
        }

        [Fact]
        public async Task CreateUser_InvalidRole_ShouldSetUserRoleToRegularByDefault()
        {
            // arrange

            // act
            var createdUser = await _sut.CreateUserAsync(TestUsername, TestPassword, TestFirstName, 
                TestLastName, TestEmail, InvalidRole);

            // assert
            _userManagerStub.Verify(um => um.AddUserToRoleAsync(createdUser, "Regular"), Times.Once);
        }
        #endregion

        #region GetUserById Test Methods
        [Fact]
        public async Task GetUserById_ValidId_ShouldReturnUserWithSameId()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(new User { Id = testUserId });

            // act
            var resultUser = await _sut.GetUserByIdAsync(testUserId);

            // assert
            Assert.NotNull(resultUser);
            Assert.IsType<User>(resultUser);
            Assert.Equal(testUserId, resultUser.Id);
        }

        [Fact]
        public async Task GetUserById_InvalidId_ShouldThrowUserNotFoundException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByIdAsync(invalidUserId)).ReturnsAsync(() => null);

            // act

            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.GetUserByIdAsync(invalidUserId));
        }
        #endregion

        #region GetUserByUsername Test Methods
        [Fact]
        public async Task GetUserByUsername_ValidUsername_ShouldReturnUserWithSameUsername()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByUserNameAsync(TestUsername)).ReturnsAsync(new User { UserName = TestUsername });

            // act
            var resultUser = await _sut.GetUserByUsernameAsync(TestUsername);

            // assert
            Assert.NotNull(resultUser);
            Assert.IsType<User>(resultUser);
            Assert.Equal(TestUsername, resultUser.UserName);
        }

        [Fact]
        public async Task GetUserByUsername_InvalidUsername_ShouldThrowUserNameNotFoundException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByUserNameAsync(TestUsername)).ReturnsAsync(() => null);

            // act

            // assert
            await Assert.ThrowsAsync<UserNameNotFoundException>(() => _sut.GetUserByUsernameAsync(TestUsername));
        }
        #endregion

        #region GetUserByEmail Test Methods
        [Fact]
        public async Task GetUserByEmail_ValidEmail_ShouldReturnUserWithSameEmail()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(new User { Email = TestEmail });

            // act
            var resultUser = await _sut.GetUserByEmailAsync(TestEmail);

            // assert
            Assert.NotNull(resultUser);
            Assert.IsType<User>(resultUser);
            Assert.Equal(TestEmail, resultUser.Email);
        }

        [Fact]
        public async Task GetUserByEmail_InvalidEmail_ShouldThrowUserEmailNotFoundException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(() => null);

            // act

            // assert
            await Assert.ThrowsAsync<UserEmailNotFoundException>(() => _sut.GetUserByEmailAsync(TestEmail));
        }
        #endregion

        #region GetCurrentUser Test Methods
        [Fact]
        public async Task GetCurrentUser_ValidData_ShouldReturnCurrentUser()
        {
            // arrange
            _userManagerStub.Setup(um => um.GetUserAsync(ClaimsPrincipal.Current)).ReturnsAsync(new User { Id = testUserId });

            // act
            var resultUser = await _sut.GetCurrentUserAsync(ClaimsPrincipal.Current);

            // assert
            Assert.NotNull(resultUser);
            Assert.IsType<User>(resultUser);
            Assert.Equal(testUserId, resultUser.Id);
        }
        #endregion

        #region GetAllUsers Test Methods
        [Fact]
        public async Task GetAllUsers_ValidData_ShouldReturnNonEmptyListOfUsers()
        {
            // arrange
            _userManagerStub.Setup(um => um.GetAllAsync()).ReturnsAsync(new List<User> { new User() });

            // act
            var users = await _sut.GetAllUsersAsync();

            // assert
            Assert.NotEmpty(users);
        }
        #endregion

        #region EditUser Test Methods
        [Fact]
        public async Task EditUser_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = TestUsername };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act
            await _sut.EditUserAsync(testUserId, TestUsername, It.IsAny<string>(), It.IsAny<string>());

            // assert
            _userManagerStub.Verify(um => um.EditUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task EditUser_InvalidUserId_ShouldThrowUserNotFoundException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.EditUserAsync(invalidUserId, It.IsAny<string>(), 
                It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task EditUser_AlreadyExistingUsername_ShouldThrowUserNameAlreadyExistsException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = TestUsername };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.FindByUserNameAsync(TestUsername)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<UserNameAlreadyExistsException>(() => _sut.EditUserAsync(testUserId, user.UserName, 
                It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task EditUser_SelectedUserIsAdmin_ShouldThrowIncorrectAdminModificationException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = "admin" };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<IncorrectAdminModificationException>(() => _sut.EditUserAsync(testUserId, user.UserName, 
                It.IsAny<string>(), It.IsAny<string>()));
        }
        #endregion

        #region ChangeUserEmail Test Methods
        [Fact]
        public async Task ChangeUserEmail_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId, Email = TestEmail, EmailConfirmed = true };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.GenerateChangeEmailTokenAsync(user, NewTestEmail)).ReturnsAsync(TestEmailToken);
            _userManagerStub.Setup(um => um.ChangeEmailAsync(user, NewTestEmail, TestEmailToken)).ReturnsAsync(IdentityResult.Success);

            // act
            await _sut.ChangeUserEmailAsync(testUserId, NewTestEmail);

            // assert
            _userManagerStub.Verify(um => um.ChangeEmailAsync(user, NewTestEmail, TestEmailToken), Times.Once);
        }

        [Fact]
        public async Task ChangeUserEmail_InvalidUserId_ShouldThrowUserNotFoundException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.ChangeUserEmailAsync(invalidUserId, It.IsAny<string>()));
        }

        [Fact]
        public async Task ChangeUserEmail_AlreadyExistingEmail_ShouldThrowUserEmailAlreadyExistsException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(new User());
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(new User());

            // act

            // assert
            await Assert.ThrowsAsync<UserEmailAlreadyExistsException>(() => _sut.ChangeUserEmailAsync(testUserId, TestEmail));
        }

        [Fact]
        public async Task ChangeUserEmail_SelectedUserIsAdmin_ShouldThrowIncorrectAdminModificationException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = "admin", Email = TestEmail };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.FindByEmailAsync(TestEmail)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<IncorrectAdminModificationException>(() => _sut.ChangeUserEmailAsync(testUserId, It.IsAny<string>()));
        }
        #endregion

        #region ChangeUserPassword Test Methods
        [Fact]
        public async Task ChangeUserPassword_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = TestUsername };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.ResetPasswordAsync(user, It.IsAny<string>(), 
                It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // act
            await _sut.ChangeUserPasswordAsync(testUserId, It.IsAny<string>());

            // assert
            _userManagerStub.Verify(um => um.ResetPasswordAsync(user, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ChangeUserPassword_InvalidUserId_ShouldThrowUserNotFoundException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.ChangeUserPasswordAsync(invalidUserId, It.IsAny<string>()));
        }

        [Fact]
        public async Task ChangeUserPassword_SelectedUserIsAdmin_ShouldThrowIncorrectAdminModificationException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = "admin" };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<IncorrectAdminModificationException>(() => _sut.ChangeUserPasswordAsync(testUserId, 
                It.IsAny<string>()));
        }

        [Fact]
        public async Task ChangeUserPassword_InvalidPassword_ShouldThrowIncorrectPasswordException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = TestUsername };

            var failedIdentityResult = IdentityResult.Failed(new IdentityError());

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.ResetPasswordAsync(user, It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(failedIdentityResult);

            // act

            // assert
            await Assert.ThrowsAsync<IncorrectPasswordException>(() => _sut.ChangeUserPasswordAsync(testUserId, 
                It.IsAny<string>()));
        }
        #endregion

        #region ChangeUserRole Test Methods
        [Fact]
        public async Task ChangeUserRole_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.IsUserInRoleAsync(testUserId, "Regular")).ReturnsAsync(true);
            _userManagerStub.Setup(um => um.RemoveUserFromRoleAsync(user, "Regular")).ReturnsAsync(IdentityResult.Success);
            _userManagerStub.Setup(um => um.AddUserToRoleAsync(user, "Regular")).ReturnsAsync(IdentityResult.Success);

            // act
            await _sut.ChangeUserRoleAsync(testUserId, "Regular", "Admin");

            // assert
            _userManagerStub.Verify(um => um.RemoveUserFromRoleAsync(user, "Regular"), Times.Once);
            _userManagerStub.Verify(um => um.AddUserToRoleAsync(user, "Admin"), Times.Once);
        }

        [Fact]
        public async Task ChangeUserRole_InvalidUserId_ShouldThrowUserNotFoundException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            // act

            // assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.ChangeUserRoleAsync(invalidUserId, 
                It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task ChangeUserRole_UserNotInCurrentRole_ShouldThrowUserInRoleNotFoundException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(new User());
            _userManagerStub.Setup(um => um.IsUserInRoleAsync(testUserId, "Admin")).ReturnsAsync(false);

            // act

            // assert
            await Assert.ThrowsAsync<UserInRoleNotFoundException>(() => _sut.ChangeUserRoleAsync(testUserId, 
                "Admin", It.IsAny<string>()));
        }

        [Fact]
        public async Task ChangeUserRole_UserAlreadyInRole_ShouldThrowUserIsAlreadyInRoleException()
        {
            // arrange
            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(new User());
            _userManagerStub.Setup(um => um.IsUserInRoleAsync(testUserId, "Regular")).ReturnsAsync(true);

            // act

            // assert
            await Assert.ThrowsAsync<UserIsAlreadyInRoleException>(() => _sut.ChangeUserRoleAsync(testUserId,
                "Regular", "Regular"));
        }

        [Fact]
        public async Task ChangeUserRole_SelectedUserIsAdmin_ShouldThrowIncorrectAdminModificationException()
        {
            // arrange
            var user = new User { Id = testUserId, UserName = "admin" };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);
            _userManagerStub.Setup(um => um.IsUserInRoleAsync(testUserId, "Admin")).ReturnsAsync(true);

            // act

            // assert
            await Assert.ThrowsAsync<IncorrectAdminModificationException>(() => _sut.ChangeUserRoleAsync(testUserId,
                "Admin", "Regulars"));
        }
        #endregion

        #region DeleteUser Test Methods
        [Fact]
        public async Task DeleteUser_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            //act
            await _sut.DeleteUserAsync(user.Id, testAdminId);

            //assert
            _userManagerStub.Verify(um => um.DeleteUserAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_InvalidUserId_ShouldThrowUserNotFoundException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            //act

            //assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => _sut.DeleteUserAsync(invalidUserId, testUserId));
        }

        [Fact]
        public async Task DeleteUser_UserAttemptsToDeleteItself_ShouldThrowIncorrectDeletionException()
        {
            // arrange
            var user = new User { Id = testUserId };

            _userManagerStub.Setup(um => um.FindByIdAsync(testUserId)).ReturnsAsync(user);

            //act

            //assert
            await Assert.ThrowsAsync<IncorrectDeletionException>(() => _sut.DeleteUserAsync(testUserId, testUserId));
        }

        [Fact]
        public async Task DeleteUser_SelectedUserIsAdmin_ShouldThrowIncorrectAdminModificationException()
        {
            // arrange
            var user = new User { Id = testAdminId, UserName = "admin" };

            _userManagerStub.Setup(um => um.FindByIdAsync(testAdminId)).ReturnsAsync(user);

            //act

            //assert
            await Assert.ThrowsAsync<IncorrectAdminModificationException>(() => _sut.DeleteUserAsync(user.Id, testUserId));
        }
        #endregion

        #region IsUserInRole Test Methods
        [Fact]
        public async Task IsUserInRole_ValidData_ShouldVerifyMethodExecution()
        {
            // arrange
            var user = new User { Id = testUserId };

            //act
            await _sut.IsUserInRoleAsync(testUserId, "Regular");

            //assert
            _userManagerStub.Verify(um => um.IsUserInRoleAsync(testUserId, "Regular"), Times.Once);
        }
        #endregion
    }
}

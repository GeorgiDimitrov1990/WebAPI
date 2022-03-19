using AutoMapper;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.Models.DTO.Requests.User;
using CoinMapWebAPI.Models.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CoinMapWebAPI.Controllers
{
    [Route("api/users")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDTO user)
        {
            var createdUser = await _userService.CreateUserAsync(user.Username, user.Password, user.FirstName, user.LastName, user.Email, user.Role);

            return CreatedAtAction(nameof(GetUser), new { userId = createdUser.Id }, _mapper.Map<UserResponseDTO>(createdUser));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            User dbUser = await _userService.GetUserByIdAsync(userId);

            return Ok(_mapper.Map<UserResponseDTO>(dbUser));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
		{
            var dbUsers = await _userService.GetAllUsersAsync();

            return Ok(_mapper.Map<List<UserResponseDTO>>(dbUsers));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> EditUser(string userId, EditUserRequestDTO user)
        {
            await _userService.EditUserAsync(userId, user.Username, user.FirstName, user.LastName);

            return Ok();
        }

        [HttpPatch("{userId}/change-email")]
        public async Task<IActionResult> ChangeUserEmail(string userId, ChangeUserEmailRequestDTO userEmail)
        {
            await _userService.ChangeUserEmailAsync(userId, userEmail.Email);

            return Ok();
        }

        [HttpPatch("{userId}/change-password")]
        public async Task<IActionResult> ChangeUserPassword(string userId, ChangeUserPasswordRequestDTO userPassword)
        {
            await _userService.ChangeUserPasswordAsync(userId, userPassword.NewPassword);

            return Ok();
        }

        [HttpPatch("{userId}/change-role")]
        public async Task<IActionResult> ChangeUserRole(string userId, ChangeUserRoleRequestDTO userRole)
        {
            await _userService.ChangeUserRoleAsync(userId, userRole.CurrentRole, userRole.NewRole);

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            User currentUser = await _userService.GetCurrentUserAsync(User);

            await _userService.DeleteUserAsync(userId, currentUser.Id);

            return Ok();
        }
    }
}

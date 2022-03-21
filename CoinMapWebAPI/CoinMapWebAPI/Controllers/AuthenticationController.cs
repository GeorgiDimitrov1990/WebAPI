using System.Net.Http;
using System.Threading.Tasks;
using CoinMapWebAPI.Authentication;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.Models.DTO.Requests.User;
using Microsoft.AspNetCore.Mvc;

namespace CoinMapWebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RetrieveToken(LoginUserRequestDTO user)
        {
            using var httpClientHandler = new HttpClientHandler();
            using var httpClient = new HttpClient(httpClientHandler);

            var token = await _userService.GetToken(httpClient, user.UsernameOrEmail, user.Password);

            return Ok(token);
        }
    }
}

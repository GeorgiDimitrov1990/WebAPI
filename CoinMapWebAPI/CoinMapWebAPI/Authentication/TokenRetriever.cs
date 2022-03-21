using CoinMapWebAPI.BLL.Services.Exceptions.Unauthorized;
using CoinMapWebAPI.BLL.Services.Intefaces;
using CoinMapWebAPI.Models.DTO.Responses;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace CoinMapWebAPI.Authentication
{
    public static class TokenRetriever
    {
        public static async Task<TokenResponseDTO> GetToken(this IUserService userService, HttpClient httpClient, string username, string password)
        {
            var requestBody = new Dictionary<string, string>
                {
                    {"username", username},
                    {"password", password },
                    {"grant_type", "password"},
                    {"client_id", "CoinMapWebAPI"},
                    {"client_secret", "secret"},
                };

            var address = @"https://localhost:5001/connect/token";
            var response = await httpClient.PostAsync(address, new FormUrlEncodedContent(requestBody));
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<TokenResponseDTO>(content);

            if (token.AccessToken == null)
                throw new UnauthorizedLoginException("Invalid Credentials.");

            return token;
        }
    }
}

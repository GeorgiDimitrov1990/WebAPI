using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
    public class LoginUserRequestDTO
    {
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string UsernameOrEmail { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}

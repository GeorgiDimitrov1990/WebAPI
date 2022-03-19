using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
	public class ChangeUserEmailRequestDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}

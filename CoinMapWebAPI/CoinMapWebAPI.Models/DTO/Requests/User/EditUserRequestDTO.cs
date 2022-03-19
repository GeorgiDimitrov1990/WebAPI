using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
	public class EditUserRequestDTO
	{
		[Required]
		[MinLength(5)]
		[MaxLength(20)]
		public string Username { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(30)]
		public string LastName { get; set; }
	}
}

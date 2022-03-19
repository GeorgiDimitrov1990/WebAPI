using CoinMapWebAPI.Models.DTO.Requests.Validation;
using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
    public class ChangeUserRoleRequestDTO
	{
		[Required]
		[RoleRange]
		public string CurrentRole { get; set; }

		[Required]
		[RoleRange]
		public string NewRole { get; set; }
	}
}

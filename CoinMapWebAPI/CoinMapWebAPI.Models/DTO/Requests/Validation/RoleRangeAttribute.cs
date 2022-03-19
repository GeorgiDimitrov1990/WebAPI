using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.Validation
{
    public class RoleRangeAttribute : ValidationAttribute
    {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value.ToString() == "Admin" || value.ToString() == "Regular")
				return ValidationResult.Success;

			return new ValidationResult($"Please enter 'Admin' or 'Regular' for Role field.");
		}
	}
}

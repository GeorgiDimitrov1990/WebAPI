using CoinMapWebAPI.Models.DTO.Requests.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
    public class CreateUserRequestDTO : IValidatableObject
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValid]
        [MinLength(8)]
        [MaxLength(20)]
        public string Password { get; set; }

        [RequiredValid]
        [MinLength(8)]
        [MaxLength(20)]
        public string RepeatPassword { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [RoleRange]
        public string Role { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            if (Password != RepeatPassword)
                result.Add(new ValidationResult("Passwords do not match!", new string[] { "Password" }));

            return result;
        }
    }
}

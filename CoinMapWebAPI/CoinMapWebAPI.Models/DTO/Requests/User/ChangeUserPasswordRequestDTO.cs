using CoinMapWebAPI.Models.DTO.Requests.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.User
{
    public class ChangeUserPasswordRequestDTO : IValidatableObject
    {
        [RequiredValid]
        [MinLength(8)]
        [MaxLength(20)]
        public string NewPassword { get; set; }

        [RequiredValid]
        [MinLength(8)]
        [MaxLength(20)]
        public string RepeatPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();

            if (NewPassword != RepeatPassword)
                result.Add(new ValidationResult("Passwords do not match!", new string[] { "NewPassword" }));

            return result;
        }
    }
}

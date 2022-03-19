using System.ComponentModel.DataAnnotations;

namespace CoinMapWebAPI.Models.DTO.Requests.Validation
{
    public class RequiredValidAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}

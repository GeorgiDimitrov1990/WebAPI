using CoinMapWebAPI.DAL.Entities;
using CoinMapWebAPI.DAL.Repositories.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoinMapWebAPI.Validators
{
    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserManager userManager;

        public PasswordValidator(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = await userManager.FindByUserNameAsync(context.UserName) ?? await userManager.FindByEmailAsync(context.UserName);

            if (user != null)
            {
                bool authResult = await userManager.ValidateUserCredentialsAsync(user.UserName, context.Password);
                if (authResult)
                {
                    List<string> roles = await userManager.GetUserRolesAsync(user);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    context.Result = new GrantValidationResult(subject: user.Id, authenticationMethod: "password", claims: claims);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials.");
                }

                return;
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid Credentials.");
        }
    }
}

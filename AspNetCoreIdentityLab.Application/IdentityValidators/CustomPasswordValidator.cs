using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AspNetCoreIdentityLab.Application.IdentityValidators
{
    public class CustomPasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            if (string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameAsPassword",
                    Description = "You cannot use your username as your password"
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

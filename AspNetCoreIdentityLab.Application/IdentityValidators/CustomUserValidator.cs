using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Application.IdentityValidators
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            const int userNameMinLength = 6;

            if (user.UserName.Length < userNameMinLength)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UsernameLength",
                    Description = "The Username must be at least 6 characters."
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var user = context.User;
            var hasPermissionClaim = user.HasClaim(Constants.PolicyType, requirement.PermissionName);

            if (hasPermissionClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

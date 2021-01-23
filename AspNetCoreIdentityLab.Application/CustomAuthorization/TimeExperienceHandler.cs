using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Application.CustomAuthorization
{
    public class TimeExperienceHandler : AuthorizationHandler<TimeExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TimeExperienceRequirement requirement)
        {
            var user = context.User;
            var timeExperienceClaim = user.FindFirst("TimeExperience");

            if (timeExperienceClaim != null)
            {
                var timeExperience = int.Parse(timeExperienceClaim?.Value);
                if (timeExperience >= requirement.TimeExperience)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Application.CustomAuthorization
{
    public class TimeExperienceRequirement : IAuthorizationRequirement
    {
        public int TimeExperience { get; }

        public TimeExperienceRequirement(int timeExperience)
        {
            TimeExperience = timeExperience;
        }
    }
}

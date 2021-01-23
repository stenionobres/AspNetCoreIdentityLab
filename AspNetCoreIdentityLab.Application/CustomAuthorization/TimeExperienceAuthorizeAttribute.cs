using System;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Application.CustomAuthorization
{
    public class TimeExperienceAuthorizeAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "TimeExperience_";

        public TimeExperienceAuthorizeAttribute(TimeExperience timeExperience)
        {
            TimeExperience = timeExperience;
        }

        public TimeExperience TimeExperience
        {
            get 
            {
                var timeExperience = (TimeExperience)Enum.Parse(typeof(TimeExperience), Policy.Substring(POLICY_PREFIX.Length));

                return timeExperience;
            }

            set { Policy = $"{POLICY_PREFIX}{value.ToString()}"; }
        }
    }
}

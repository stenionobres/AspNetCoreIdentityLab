using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Application.CustomAuthorization
{
    public class CustomPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        const string TIME_EXPERIENCE_POLICY_PREFIX = "TimeExperience_";

        public CustomPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(TIME_EXPERIENCE_POLICY_PREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var timeExperience = (TimeExperience)Enum.Parse(typeof(TimeExperience), policyName.Substring(TIME_EXPERIENCE_POLICY_PREFIX.Length));

                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new TimeExperienceRequirement(Convert.ToInt32(timeExperience)));
                
                return policy.Build();
            }

            return await base.GetPolicyAsync(policyName);
        }
    }
}

using System.Collections.Generic;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Model
{
    public class ResourceModel
    {
        public string Description { get; set; }
        public List<ResourceModel> Resources { get; set; } = new List<ResourceModel>();
        public List<PolicyModel> Policies { get; set; } = new List<PolicyModel>();

        public ResourceModel(string description, IEnumerable<Permissions> permissions)
        {
            Description = description;
            MountPolicies(permissions);
        }

        private void MountPolicies(IEnumerable<Permissions> permissions)
        {
            foreach (var permission in permissions)
            {
                Policies.Add(new PolicyModel(permission));
            }
        }

    }
}

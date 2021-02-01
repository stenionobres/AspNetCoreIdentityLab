using System;
using System.Collections.Generic;
using System.Reflection;
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
                if (IsNotObsolete(permission))
                {
                    Policies.Add(new PolicyModel(permission));
                }
            }
        }

        private bool IsNotObsolete(Permissions permission)
        {
            var enumType = permission.GetType();
            var enumMember = enumType.GetMember(permission.ToString());
            var obsoleteAttribute = enumMember[0].GetCustomAttribute<ObsoleteAttribute>();

            return obsoleteAttribute == null;
        }
    }
}

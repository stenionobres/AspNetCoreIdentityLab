using System;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Model
{
    public class PolicyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public PolicyModel(Permissions permission)
        {
            Id = Convert.ToInt32(permission);
            Description = permission.ToString();
        }
    }
}

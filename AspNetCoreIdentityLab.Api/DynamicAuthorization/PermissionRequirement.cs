using System;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }

        public PermissionRequirement(string permissionName)
        {
            PermissionName = permissionName ?? throw new ArgumentNullException(nameof(permissionName));
        }
    }
}

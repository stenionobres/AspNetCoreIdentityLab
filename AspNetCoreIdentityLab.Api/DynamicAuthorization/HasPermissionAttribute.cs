using System;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission) : base(policy: permission.ToString())
        {

        }
    }
}

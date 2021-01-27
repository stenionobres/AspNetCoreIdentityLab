using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public enum Permissions
    {
        [Display(GroupName = "User", Name = UserAction.Read, Description = "Can read a User")]
        CanReadUser = 1,

        [Display(GroupName = "User", Name = UserAction.Update, Description = "Can update a User")]
        CanUpdateUser,


        [Display(GroupName = "Role", Name = UserAction.Create, Description = "Can create a Role")]
        CanCreateRole,

        [Display(GroupName = "Role", Name = UserAction.Delete, Description = "Can delete a Role")]
        CanDeleteRole,

        [Display(GroupName = "Reports", Name = "PrintReports", Description = "Can print reports")]
        CanPrintReports
    }
}

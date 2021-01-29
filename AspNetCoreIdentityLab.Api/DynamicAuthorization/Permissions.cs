using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public enum Permissions
    {
        [Display(Description = UserAction.Read)]
        CanReadUser = 1,

        [Display(Description = UserAction.Update)]
        CanUpdateUser,


        [Display(Description = UserAction.Create)]
        CanCreateRole,

        [Display(Description = UserAction.Delete)]
        CanDeleteRole,

        [Display(Description = "PrintReports")]
        CanPrintReports
    }
}

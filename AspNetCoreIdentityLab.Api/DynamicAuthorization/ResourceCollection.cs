
namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public class ResourceCollection
    {
        public static Resource Get()
        {
            return new Resource("Menu").Add(
                new Resource("Analytics").Add(
                    new Resource("Users Registration").Add(
                        Permissions.CanReadUser,
                        Permissions.CanUpdateUser
                    ),
                    new Resource("Roles Registration").Add(
                        Permissions.CanCreateRole,
                        Permissions.CanDeleteRole
                    )
                ),
                new Resource("Reports", Permissions.CanPrintReports)
            );
        }
    }
}


namespace AspNetCoreIdentityLab.Application.Models
{
    public class ImpersonateUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsImpersonated { get; set; }
    }
}

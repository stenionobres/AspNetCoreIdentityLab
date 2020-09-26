using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityLab.Persistence.DataTransferObjects
{
    public class User : IdentityUser<int>
    {
        public string Occupation { get; set; }
    }
}

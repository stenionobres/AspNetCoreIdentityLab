using System.Collections.Generic;

namespace AspNetCoreIdentityLab.Application.Models
{
    public class ImpersonateModel
    {
        public string Message { get; set; }
        public List<ImpersonateUserModel> Users { get; set; } = new List<ImpersonateUserModel>();
    }
}

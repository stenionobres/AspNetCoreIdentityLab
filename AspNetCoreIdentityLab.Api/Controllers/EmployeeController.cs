using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HasPermission(Permissions.CanCreateEmployee)]
        public ActionResult Save()
        {
            return Ok("CreateEmployee Authorized");
        }

        [HasPermission(Permissions.CanReadEmployee)]
        public ActionResult Get()
        {
            return Ok("ReadEmployee Authorized");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HasPermission(Permissions.CanCreateEmployee)]
        public ActionResult Save()
        {
            return Ok();
        }

        [HasPermission(Permissions.CanReadEmployee)]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
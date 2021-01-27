using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentityLab.Api.Model;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<ResourceCollectionModel> GetAll()
        {
            return Ok(new ResourceCollectionModel());
        }
    }
}
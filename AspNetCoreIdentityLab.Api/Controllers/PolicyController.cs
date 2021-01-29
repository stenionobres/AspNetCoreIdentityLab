using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Api.Services;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly PolicyService _policyService;

        public PolicyController(PolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpPost]
        public ActionResult Save(PolicyModel policyModel)
        {
            _policyService.Save(policyModel);

            return Ok("Policy saved");
        }
    }
}
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleClaimsController(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult> Save(RoleClaimModel roleClaimModel)
        {
            var claim = new Claim(Constants.PolicyType, roleClaimModel.PolicyId.ToString());
            var role = await _roleManager.FindByIdAsync(roleClaimModel.RoleId.ToString());

            if (role == null)
            {
                return Problem(detail: "There is no role with this id", instance: null, statusCode: 500);
            }

            var identityResult = await _roleManager.AddClaimAsync(role, claim);

            if (identityResult.Succeeded)
            {
                return Ok("Claim added to Role");
            }

            var errorDetail = identityResult.Errors.First().Description;

            return Problem(detail: errorDetail, instance: null, statusCode: 500);
        }
    }
}
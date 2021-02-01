using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Api.DynamicAuthorization;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserClaimsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Save(UserClaimModel userClaimModel)
        {
            var claim = new Claim(Constants.PolicyType, userClaimModel.PolicyId.ToString());
            var user = await _userManager.FindByIdAsync(userClaimModel.UserId.ToString());

            if (user == null)
            {
                return Problem(detail: "There is no user with this id", instance: null, statusCode: 500);
            }

            var identityResult = await _userManager.AddClaimAsync(user, claim);

            if (identityResult.Succeeded)
            {
                return Ok("Claim added to User");
            }

            var errorDetail = identityResult.Errors.First().Description;

            return Problem(detail: errorDetail, instance: null, statusCode: 500);
        }
    }
}
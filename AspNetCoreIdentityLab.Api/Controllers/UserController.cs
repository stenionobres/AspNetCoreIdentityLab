using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel userModel)
        {
            var user = new User() { UserName = userModel.Email, Email = userModel.Email, Occupation = userModel.Occupation };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
            {
                return Created(string.Empty, new { user.Id, user.Email, user.Occupation });
            }

            var errorDetail = result.Errors.First().Description;

            return Problem(detail: errorDetail, instance: null, statusCode: 500);
        }
    }
}
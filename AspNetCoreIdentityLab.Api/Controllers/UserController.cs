using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Api.Model;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using AspNetCoreIdentityLab.Api.Jwt;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;

        public UserController(UserManager<User> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var user = new User() { UserName = signUpModel.Email, Email = signUpModel.Email, Occupation = signUpModel.Occupation };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (result.Succeeded)
            {
                return Created(string.Empty, new { user.Id, user.Email, user.Occupation });
            }

            var errorDetail = result.Errors.First().Description;

            return Problem(detail: errorDetail, instance: null, statusCode: 500);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == signInModel.Email);
            
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userSignInResult = await _userManager.CheckPasswordAsync(user, signInModel.Password);

            if (userSignInResult)
            {
                return Ok(new { Token = _jwtService.GenerateToken() });
            }

            return BadRequest("Email or password incorrect.");
        }
    }
}
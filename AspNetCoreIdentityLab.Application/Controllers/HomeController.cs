using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreIdentityLab.Application.Models;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreIdentityLab.Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string message, string impersonateUserId)
        {
            var users = _userManager.Users.ToList();

            var impersonateModel = new ImpersonateModel();
            impersonateModel.Message = message;

            foreach (var user in users)
            {
                var impersonateUserModel = new ImpersonateUserModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    IsImpersonated = user.Id.ToString() == impersonateUserId
                };
                
                impersonateModel.Users.Add(impersonateUserModel);
            }

            return View(impersonateModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ImpersonateUser(string userId)
        {
            var currentUserNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier);
            var currentUserId = currentUserNameIdentifier?.Value;

            if (currentUserId == userId)
            {
                var impersonateUserId = User.FindFirst("ImpersonateUserId")?.Value;

                return RedirectToAction("Index", "Home", new { message = "Error You cannot impersonate yourself", impersonateUserId });
            }

            if (User.HasClaim("IsImpersonating", "true"))
            {
                var impersonateUserId = User.FindFirst("ImpersonateUserId")?.Value;

                return RedirectToAction("Index", "Home", new { message = "Error You are already in impersonation mode", impersonateUserId });
            }

            var impersonatedUser = await _userManager.FindByIdAsync(userId);
            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(impersonatedUser);
            var firstIdentity = userPrincipal.Identities.FirstOrDefault();
            var originalUserIdClaim = new Claim("OriginalUserId", currentUserId);
            var impersonateUserIdClaim = new Claim("ImpersonateUserId", userId);
            var isImpersonatingClaim = new Claim("IsImpersonating", "true");

            firstIdentity.AddClaim(originalUserIdClaim);
            firstIdentity.AddClaim(impersonateUserIdClaim);
            firstIdentity.AddClaim(isImpersonatingClaim);

            await _signInManager.SignOutAsync();

            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, userPrincipal);

            return RedirectToAction("Index", "Home", new { message = "User impersonation started", impersonateUserId = userId });
        }

        public async Task<IActionResult> StopImpersonation()
        {
            if (!User.HasClaim("IsImpersonating", "true"))
            {
                RedirectToAction("Index", "Home", new { message = "Error You are not impersonating now. Can't stop impersonation", impersonateUserId = "" });
            }

            var originalUserId = User.FindFirst("OriginalUserId").Value;
            var originalUser = await _userManager.FindByIdAsync(originalUserId);

            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(originalUser, isPersistent: true);

            return RedirectToAction("Index", "Home", new { message = "User impersonation stopped", impersonateUserId = "" });
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;

namespace AspNetCoreIdentityLab.Application.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ForgotPasswordConfirmation(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string Email { get; set; }
        public string ConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string confirmationUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            ConfirmationUrl = confirmationUrl;

            return Page();
        }
    }
}

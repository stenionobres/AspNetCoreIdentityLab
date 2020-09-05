using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AspNetCoreIdentityLab.Persistence.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreIdentityLab.Application.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public RegisterConfirmationModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

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

            ConfirmationUrl = confirmationUrl;

            return Page();
        }
    }
}

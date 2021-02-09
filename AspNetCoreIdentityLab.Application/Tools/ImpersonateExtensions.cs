using System.Linq;
using Microsoft.AspNetCore.Html;
using System.Security.Claims;

namespace AspNetCoreIdentityLab.Application.Tools
{
    public static class ImpersonateExtensions
    {
        public static HtmlString GetCurrentUserNameAsHtml(this ClaimsPrincipal claimsPrincipal)
        {
            var isImpersonating = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == "IsImpersonating")?.Value;
            var nameToShow = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            return new HtmlString(
                "<span" + (isImpersonating != null ? " class=\"text-danger\">Impersonating " : ">Hello ")
                     + $"{nameToShow}</span>");
        }
    }
}

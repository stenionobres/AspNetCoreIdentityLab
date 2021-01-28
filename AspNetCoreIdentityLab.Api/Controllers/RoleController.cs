using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityLab.Api.Model;

namespace AspNetCoreIdentityLab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RoleController(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleModel>> GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            var rolesModel = roles.Select(role => new RoleModel() { Id = role.Id, Name = role.Name });

            return Ok(rolesModel);
        }

        [HttpPost]
        public async Task<ActionResult> Save(RoleModel roleModel)
        {
            var identityRole = new IdentityRole<int>() { Name = roleModel.Name };
            var identityResult = await _roleManager.CreateAsync(identityRole);
            roleModel.Id = identityRole.Id;

            if (identityResult.Succeeded)
            {
                return Ok(roleModel);
            }

            var errorDetail = identityResult.Errors.First().Description;

            return Problem(detail: errorDetail, instance: null, statusCode: 500);
        }
    }
}
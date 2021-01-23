using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentityLab.Application.CustomAuthorization;

namespace AspNetCoreIdentityLab.Application.Controllers
{
    [Authorize(Policy = "AtLeastFiveYearsExperience")]
    public class BackupController : Controller
    {
        public ActionResult GetLastBackup()
        {
            return Ok("GetLastBackup");
        }

        [Authorize(Policy = "AtLeastSevenYearsExperience")]
        public ActionResult RebuildIndexes()
        {
            return Ok("RebuildIndexes");
        }

        [TimeExperienceAuthorize(TimeExperience.LEVEL_THREE)]
        public ActionResult RemoveBackup()
        {
            return Ok("RemoveBackup");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    }
}
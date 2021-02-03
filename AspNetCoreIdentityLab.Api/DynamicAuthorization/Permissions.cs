using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public enum Permissions
    {
        CanAccessLogistics = 1,
        CanAccessAccounting,

        [Display(Description = UserAction.Create)]
        CanCreateEmployee,

        [Display(Description = UserAction.Delete)]
        CanDeleteEmployee,

        [Display(Description = UserAction.Read)]
        CanReadEmployee,

        [Display(Description = UserAction.Update)]
        CanUpdateEmployee,


        [Display(Description = UserAction.Create)]
        CanCreateDependents,

        [Display(Description = UserAction.Delete)]
        CanDeleteDependents,

        [Display(Description = UserAction.Read)]
        CanReadDependents,

        [Display(Description = UserAction.Update)]
        CanUpdateDependents,


        [Display(Description = UserAction.Create)]
        CanCreateVacationPlanning,

        [Display(Description = UserAction.Read)]
        CanReadVacationPlanning,


        CanAccessPaymentHistory,

        CanAccessTimeReport,
        CanAccessCostsReport,
        CanAccessVacationByPeriod,


        [Display(Description = UserAction.Create)]
        CanCreateTaskManager,

        [Display(Description = UserAction.Delete)]
        CanDeleteTaskManager,

        [Display(Description = UserAction.Read)]
        CanReadTaskManager,

        [Display(Description = UserAction.Update)]
        CanUpdateTaskManager,

        CanAccessTaxCalculator
    }
}

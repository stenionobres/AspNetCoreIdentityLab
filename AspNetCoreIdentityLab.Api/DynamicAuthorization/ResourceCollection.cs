
namespace AspNetCoreIdentityLab.Api.DynamicAuthorization
{
    public class ResourceCollection
    {
        public static Resource Get()
        {
            return new Resource("Menu").Add(
                new Resource("Logistics", Permissions.CanAccessLogistics),
                new Resource("Accounting", Permissions.CanAccessAccounting),
                new Resource("Human Resources").Add(
                    new Resource("Employees").Add(
                        new Resource("Employee Registration").Add(
                            Permissions.CanCreateEmployee,
                            Permissions.CanDeleteEmployee,
                            Permissions.CanReadEmployee,
                            Permissions.CanUpdateEmployee
                        ),
                        new Resource("Dependents Registration").Add(
                            Permissions.CanCreateDependents,
                            Permissions.CanDeleteDependents,
                            Permissions.CanReadDependents,
                            Permissions.CanUpdateDependents
                        )
                    ),
                    new Resource("Vacation").Add(
                        new Resource("Vacation Planning").Add(
                            Permissions.CanCreateVacationPlanning,
                            Permissions.CanReadVacationPlanning
                        ),
                        new Resource("Payment History", Permissions.CanAccessPaymentHistory),
                        new Resource("Reports").Add(
                            new Resource("Time Report", Permissions.CanAccessTimeReport),
                            new Resource("Costs Report", Permissions.CanAccessCostsReport),
                            new Resource("Vacation by Period", Permissions.CanAccessVacationByPeriod)
                        )
                    )
                ),
                new Resource("Tools").Add(
                    new Resource("Task Manager").Add(
                        Permissions.CanCreateTaskManager,
                        Permissions.CanDeleteTaskManager,
                        Permissions.CanReadTaskManager,
                        Permissions.CanUpdateTaskManager
                    ),
                    new Resource("Tax Calculator", Permissions.CanAccessTaxCalculator)
                )
            );
        }
    }
}

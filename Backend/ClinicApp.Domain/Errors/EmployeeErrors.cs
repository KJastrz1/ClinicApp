using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class EmployeeErrors
{
    public static Error NotFound(UserId id) => new(
        "Employees.NotFound",
        $"Employee with ID {id.Value} was not found.");

    public static Error EmptyId => new(
        "Employees.EmptyId",
        "Employee ID is empty.");
}

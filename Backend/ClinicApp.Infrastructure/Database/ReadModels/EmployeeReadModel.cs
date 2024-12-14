using Shared.Contracts.Employee;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class EmployeeReadModel
{
    internal UserReadModel User { get; set; }

    internal EmployeeResponse MapToResponse()
    {
        return new EmployeeResponse(
            Id: User.Id,
            FirstName: User.FirstName,
            LastName: User.LastName,
            UserRole: User.UserRole
        );
    }
}

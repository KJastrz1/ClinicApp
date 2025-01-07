using Shared.Contracts.Employee;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class EmployeeReadModel : UserProfileReadModel
{
    public List<EmployeeLeaveReadModel> Leaves { get; set; } = new List<EmployeeLeaveReadModel>();

    internal new EmployeeResponse MapToResponse()
    {
        return new EmployeeResponse(
            Id: Id,
            FirstName: FirstName,
            LastName: LastName,
            UserRoleEnum: UserRoleEnum
        );
    }
}

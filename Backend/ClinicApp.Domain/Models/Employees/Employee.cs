using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.UserProfiles;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;

namespace ClinicApp.Domain.Models.Employees;

public abstract class Employee : UserProfile
{
    private List<EmployeeLeave> _leaves = new List<EmployeeLeave>();
    public IReadOnlyList<EmployeeLeave> Leaves => _leaves.AsReadOnly();

    protected Employee() { }

    protected Employee(
        UserId id,
        FirstName firstName,
        LastName lastName,
        UserRole userRole
      ) 
        : base(id, firstName, lastName, userRole)
    {
    }
}

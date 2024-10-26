using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Employees;

public class Employee : User
{
    private List<EmployeeLeave> _leaves = new List<EmployeeLeave>();
    public IReadOnlyList<EmployeeLeave> Leaves => _leaves.AsReadOnly();

    protected Employee() { }

    protected Employee(
        UserId id,
        FirstName firstName,
        LastName lastName,
        UserType userType,
        Account? account = null) 
        : base(id, firstName, lastName, userType, account)
    {
    }
}

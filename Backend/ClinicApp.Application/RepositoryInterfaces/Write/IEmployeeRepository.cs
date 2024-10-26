using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.Users.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(
        UserId id,
        CancellationToken cancellationToken);
}

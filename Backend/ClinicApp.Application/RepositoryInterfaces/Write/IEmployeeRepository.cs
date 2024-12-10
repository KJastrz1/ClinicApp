using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(
        UserId id,
        CancellationToken cancellationToken);
}

using ClinicApp.Application.RepositoryInterfaces;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly WriteDbContext _writeContext;

    public EmployeeRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Employee?> GetByIdAsync(UserId id, CancellationToken cancellationToken)
    {
        Doctor? doctor = await _writeContext.Doctors
            .FirstOrDefaultAsync(d => d.Id.Value == id.Value, cancellationToken);

        if (doctor != null)
        {
            return doctor;
        }

        return null;
    }
}

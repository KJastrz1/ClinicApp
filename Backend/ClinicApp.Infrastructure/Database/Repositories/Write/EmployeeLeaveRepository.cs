using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.RepositoryInterfaces;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class EmployeeLeaveRepository : IEmployeeLeaveRepository
{
    private readonly WriteDbContext _writeContext;

    public EmployeeLeaveRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<EmployeeLeave?> GetByIdAsync(EmployeeLeaveId id, CancellationToken cancellationToken)
    {
        return await _writeContext.EmployeeLeaves
            .FirstOrDefaultAsync(el => el.Id.Value == id.Value, cancellationToken);
    }

    public void Add(EmployeeLeave employeeLeave)
    {
        _writeContext.EmployeeLeaves.Add(employeeLeave);
    }

    public void Update(EmployeeLeave employeeLeave)
    {
        _writeContext.EmployeeLeaves.Update(employeeLeave);
    }

    public void Remove(EmployeeLeave employeeLeave)
    {
        _writeContext.EmployeeLeaves.Remove(employeeLeave);
    }
    
    public async Task<bool> HasOverlappingLeavesAsync(UserId employeeId, LeaveStartDate startDate, LeaveEndDate endDate, CancellationToken cancellationToken)
    {
        return await _writeContext.EmployeeLeaves
            .AnyAsync(el => el.EmployeeId.Value == employeeId.Value &&
                            el.StartDate.Value < endDate.Value &&
                            el.EndDate.Value > startDate.Value,
                cancellationToken);
    }
}

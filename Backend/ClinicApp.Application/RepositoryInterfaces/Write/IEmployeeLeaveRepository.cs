using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;

namespace ClinicApp.Domain.RepositoryInterfaces;

public interface IEmployeeLeaveRepository
{
    Task<EmployeeLeave?> GetByIdAsync(
        EmployeeLeaveId id,
        CancellationToken cancellationToken);

    void Add(EmployeeLeave employeeLeave);

    void Update(EmployeeLeave employeeLeave);

    void Remove(EmployeeLeave employeeLeave);
    
    Task<bool> HasOverlappingLeavesAsync(
        UserId employeeId,
        LeaveStartDate startDate,
        LeaveEndDate endDate,
        CancellationToken cancellationToken);
}

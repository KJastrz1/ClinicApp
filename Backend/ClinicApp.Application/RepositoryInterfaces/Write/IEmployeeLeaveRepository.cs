using ClinicApp.Domain.Models.EmployeeLeaves;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

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

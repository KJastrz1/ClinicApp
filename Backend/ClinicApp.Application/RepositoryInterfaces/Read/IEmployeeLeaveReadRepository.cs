using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.EmployeeLeave.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.RepositoryInterfaces.Read;

public interface IEmployeeLeaveReadRepository
{
    Task<EmployeeLeaveResponse?> GetByIdAsync(EmployeeLeaveId leaveId, CancellationToken cancellationToken);
    
  
    Task<PagedItems<EmployeeLeaveResponse>> GetByFilterAsync(
        EmployeeLeaveFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
    
    Task<bool> HasOverlappingLeavesAsync(
        UserId employeeId,
        LeaveStartDate startDate,
        LeaveEndDate endDate,
        CancellationToken cancellationToken);
}

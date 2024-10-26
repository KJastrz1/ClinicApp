using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using Shared.Contracts.EmployeeLeave.Responses;
using System.Threading;
using ClinicApp.Domain.Models.Users.ValueObjects;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.ReadRepositories;

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

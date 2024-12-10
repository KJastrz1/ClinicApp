using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Shared.Contracts.EmployeeLeave.Responses;
using System.Collections.Generic;
using System.Linq;
using ClinicApp.Application.RepositoryInterfaces.Read;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Database.ReadModels;
using Shared.Contracts.Employee;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.Shared;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class EmployeeLeaveReadRepository : IEmployeeLeaveReadRepository
{
    private readonly ReadDbContext _context;

    public EmployeeLeaveReadRepository(ReadDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeLeaveResponse?> GetByIdAsync(EmployeeLeaveId leaveId, CancellationToken cancellationToken)
    {
        EmployeeLeaveReadModel? leaveReadModel = await _context.EmployeeLeaves
            .Include(el => el.Employee)
            .FirstOrDefaultAsync(l => l.Id == leaveId, cancellationToken);

        return leaveReadModel?.MapToResponse();
    }

    public async Task<PagedItems<EmployeeLeaveResponse>> GetByFilterAsync(EmployeeLeaveFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        IQueryable<EmployeeLeaveReadModel> query = _context.EmployeeLeaves
            .Include(l => l.Employee);

        if (filter.EmployeeId.HasValue)
        {
            query = query.Where(l => l.Employee.User.Id == filter.EmployeeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.EmployeeLastName))
        {
            query = query.Where(l =>
                l.Employee.User.LastName.ToLower().Contains(filter.EmployeeLastName.ToLower()));
        }

        if (filter.StartOfLeaveInRange != null && filter.StartOfLeaveInRange.StartRange.HasValue)
        {
            query = query.Where(l => l.StartDate >= filter.StartOfLeaveInRange.StartRange);
        }

        if (filter.StartOfLeaveInRange != null && filter.StartOfLeaveInRange.EndRange.HasValue)
        {
            query = query.Where(l => l.StartDate <= filter.StartOfLeaveInRange.EndRange);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        List<EmployeeLeaveResponse> leaves = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(l => l.MapToResponse())
            .ToListAsync(cancellationToken);

        return new PagedItems<EmployeeLeaveResponse>
        {
            Items = leaves,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }

    public async Task<bool> HasOverlappingLeavesAsync(UserId employeeId, LeaveStartDate startDate, LeaveEndDate endDate,
        CancellationToken cancellationToken)
    {
        return await _context.EmployeeLeaves
            .AnyAsync(el => el.EmployeeId == employeeId.Value &&
                            el.StartDate < endDate.Value &&
                            el.EndDate > startDate.Value,
                cancellationToken);
    }
}

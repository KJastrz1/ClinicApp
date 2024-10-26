using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Shared;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.EmployeeLeave.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaves;

internal sealed class GetEmployeeLeavesQueryHandler : IQueryHandler<GetEmployeeLeavesQuery, PagedItems<EmployeeLeaveResponse>>
{
    private readonly IEmployeeLeaveReadRepository _employeeLeaveReadRepository;

    public GetEmployeeLeavesQueryHandler(IEmployeeLeaveReadRepository employeeLeaveReadRepository)
    {
        _employeeLeaveReadRepository = employeeLeaveReadRepository;
    }

    public async Task<Result<PagedItems<EmployeeLeaveResponse>>> Handle(GetEmployeeLeavesQuery request, CancellationToken cancellationToken)
    {
        PagedItems<EmployeeLeaveResponse> items = await _employeeLeaveReadRepository.GetByFilterAsync(
            request.Filter,
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);

        return Result.Success(items);
    }
}

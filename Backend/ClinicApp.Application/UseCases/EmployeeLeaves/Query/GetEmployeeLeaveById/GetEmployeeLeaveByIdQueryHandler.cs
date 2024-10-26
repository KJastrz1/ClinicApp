using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.EmployeeLeave.Responses;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaveById;

internal sealed class GetEmployeeLeaveByIdQueryHandler
    : IQueryHandler<GetEmployeeLeaveByIdQuery, EmployeeLeaveResponse>
{
    private readonly IEmployeeLeaveReadRepository _employeeLeaveReadRepository;

    public GetEmployeeLeaveByIdQueryHandler(IEmployeeLeaveReadRepository employeeLeaveReadRepository)
    {
        _employeeLeaveReadRepository = employeeLeaveReadRepository;
    }

    public async Task<Result<EmployeeLeaveResponse>> Handle(
        GetEmployeeLeaveByIdQuery request,
        CancellationToken cancellationToken)
    {
        EmployeeLeaveId leaveId = EmployeeLeaveId.Create(request.LeaveId).Value;
        EmployeeLeaveResponse? employeeLeave = await _employeeLeaveReadRepository.GetByIdAsync(
            leaveId,
            cancellationToken);

        if (employeeLeave is null)
        {
            return Result.Failure<EmployeeLeaveResponse>(EmployeeLeaveErrors.NotFound(leaveId));
        }

        return employeeLeave;
    }
}

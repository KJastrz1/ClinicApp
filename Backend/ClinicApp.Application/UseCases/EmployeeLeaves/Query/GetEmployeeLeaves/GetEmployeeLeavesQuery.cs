using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.EmployeeLeave.Requests;
using Shared.Contracts.EmployeeLeave.Responses;
using Shared.Contracts.Shared;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaves;



public sealed record GetEmployeeLeavesQuery : IQuery<PagedItems<EmployeeLeaveResponse>>
{
    public EmployeeLeaveFilter Filter { get; init; } = new EmployeeLeaveFilter();
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

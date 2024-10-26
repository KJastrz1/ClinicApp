using ClinicApp.Application.Abstractions.Messaging;
using MediatR;
using Shared.Contracts.EmployeeLeave.Responses;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Query.GetEmployeeLeaveById;

public sealed record GetEmployeeLeaveByIdQuery(
    Guid LeaveId
) : IQuery<EmployeeLeaveResponse>;

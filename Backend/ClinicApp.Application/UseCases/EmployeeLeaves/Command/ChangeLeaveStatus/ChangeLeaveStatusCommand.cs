using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Enums;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.ChangeLeaveStatus;

public sealed record ChangeLeaveStatusCommand(
    Guid LeaveId,
    LeaveStatusEnum NewStatus
) : ICommand;

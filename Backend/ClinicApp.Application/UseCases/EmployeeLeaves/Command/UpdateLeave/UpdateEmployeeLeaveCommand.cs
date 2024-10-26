using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.UpdateLeave;

public sealed record UpdateEmployeeLeaveCommand(
    Guid LeaveId,
    string? Reason,
    DateTime? StartDate,
    DateTime? EndDate
) : ICommand;

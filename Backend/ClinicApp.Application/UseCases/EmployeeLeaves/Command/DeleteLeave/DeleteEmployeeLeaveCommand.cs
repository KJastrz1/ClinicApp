using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.DeleteLeave;

public sealed record DeleteEmployeeLeaveCommand(
    Guid LeaveId) : ICommand<Guid>;

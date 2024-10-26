using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.EmployeeLeaves.Command.CreateEmployeeLeave;

public sealed record CreateEmployeeLeaveCommand(
    Guid EmployeeId,
    string Reason,
    DateTime StartDate,
    DateTime EndDate
) : ICommand<Guid>;

using System;
using ClinicApp.Domain.Enums;
using ClinicApp.Infrastructure.Database.Repositories.Read;
using Shared.Contracts.Employee;
using Shared.Contracts.EmployeeLeave.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class EmployeeLeaveReadModel
{
    public Guid Id { get; set; }
    
    public Guid EmployeeId { get; set; }
    public EmployeeReadModel Employee { get; set; }
    public string Reason { get; set; }
    public LeaveStatusEnum Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal EmployeeLeaveResponse MapToResponse()
    {
        return new EmployeeLeaveResponse(
            Id: Id,
            Employee: Employee.MapToResponse(),
            Reason: Reason,
            Status: Status,
            StartDate: StartDate,
            EndDate: EndDate
        );
    }
}

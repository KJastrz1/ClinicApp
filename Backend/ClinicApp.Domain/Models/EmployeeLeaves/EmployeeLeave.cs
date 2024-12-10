using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.EmployeeLeaves.DomainEvents;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Models.Employees;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.EmployeeLeaves;

public class EmployeeLeave : AggregateRoot<EmployeeLeaveId>
{
    public UserId EmployeeId { get; private set; }
    public Employee Employee { get; private set; }
    public LeaveReason Reason { get; private set; }
    public LeaveStatusEnum Status { get; private set; }
    public LeaveStartDate StartDate { get; private set; }
    public LeaveEndDate EndDate { get; private set; }

    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }

    private EmployeeLeave() { }

    private EmployeeLeave(
        EmployeeLeaveId id,
        Employee employee,
        LeaveReason reason,
        LeaveStartDate startDate,
        LeaveEndDate endDate)
        : base(id)
    {
        Employee = employee;
        EmployeeId = employee.Id;
        Reason = reason;
        Status = LeaveStatusEnum.Requested;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static EmployeeLeave Create(
        EmployeeLeaveId employeeLeaveId,
        Employee employee,
        LeaveReason reason,
        LeaveStartDate startDate,
        LeaveEndDate endDate)
    {
        var employeeLeave = new EmployeeLeave(employeeLeaveId, employee, reason, startDate, endDate);
        employeeLeave.RaiseDomainEvent(new EmployeeLeaveCreatedDomainEvent(employeeLeaveId.Value, employee.Id.Value,
            startDate.Value, endDate.Value));
        return employeeLeave;
    }

    public Result<EmployeeLeave> ChangeLeaveDates(LeaveStartDate newStartDate, LeaveEndDate newEndDate)
    {
        if (StartDate.Equals(newStartDate) && EndDate.Equals(newEndDate))
        {
            return Result.Success(this);
        }

        StartDate = newStartDate;
        EndDate = newEndDate;
        RaiseDomainEvent(new EmployeeLeaveDatesChangedDomainEvent(Id.Value, newStartDate.Value, newEndDate.Value));

        return Result.Success(this);
    }

    public Result<EmployeeLeave> ChangeLeaveReason(LeaveReason newReason)
    {
        if (Reason.Equals(newReason))
        {
            return Result.Success(this);
        }

        Reason = newReason;
        RaiseDomainEvent(new EmployeeLeaveReasonChangedDomainEvent(Id.Value, newReason.Value));

        return Result.Success(this);
    }

    public Result<EmployeeLeave> ChangeLeaveStatus(LeaveStatusEnum newStatus)
    {
        if (Status.Equals(newStatus))
        {
            return Result.Success(this);
        }

        Status = newStatus;
        RaiseDomainEvent(new EmployeeLeaveStatusChangedDomainEvent(Id.Value, newStatus));

        return Result.Success(this);
    }

    public Result<bool> Delete()
    {
        RaiseDomainEvent(new EmployeeLeaveDeletedDomainEvent(Id.Value));
        return Result.Success(true);
    }
}

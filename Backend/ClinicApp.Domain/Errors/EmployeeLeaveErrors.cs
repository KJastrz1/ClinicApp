using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class EmployeeLeaveErrors
{
    public static Error NotFound(EmployeeLeaveId id) => new(
        "EmployeeLeaves.NotFound",
        $"Leave with ID {id.Value} was not found.");

    public static Error EmptyId => new(
        "EmployeeLeaves.EmptyId",
        "Leave ID is empty.");

    public static Error EmployeeIdRequired = new(
        "EmployeeLeaves.EmployeeIdRequired",
        "Employee ID is required.");

    public static Error EndBeforeStart = new(
        "EmployeeLeaves.EndBeforeStart",
        "End date must be after the start date.");

    public static readonly Error DateInThePast = new(
        "EmployeeLeaves.LeaveInPast",
        "Cannot schedule leave in the past.");

    public static readonly Error OverlappingLeave = new(
        "EmployeeLeaves.OverlappingLeave",
        "This leave overlaps with another existing leave.");

    public static Error InvalidStatus = new(
        "EmployeeLeaves.InvalidStatus",
        "Invalid leave status.");

    public static class ReasonErrors
    {
        public static Error Required = new(
            "EmployeeLeaves.LeaveReasonRequired",
            "Leave reason is required.");

        public static Error TooLong = new(
            "EmployeeLeaves.LeaveReasonTooLong",
            $"Leave reason cannot be longer than {LeaveReason.MaxLength} characters.");
    }
}

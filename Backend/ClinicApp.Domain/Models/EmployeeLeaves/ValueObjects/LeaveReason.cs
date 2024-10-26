using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;

public sealed class LeaveReason : ValueObject
{
    public const int MaxLength = 500;

    private LeaveReason(string value)
    {
        Value = value;
    }

    private LeaveReason() { }

    public string Value { get; private set; }

    public static Result<LeaveReason> Create(string reason) =>
        Result.Create(reason)
            .Ensure(
                r => !string.IsNullOrWhiteSpace(r),
                EmployeeLeaveErrors.ReasonErrors.Required)
            .Ensure(
                r => r.Length <= MaxLength,
                EmployeeLeaveErrors.ReasonErrors.TooLong)
            .Map(r => new LeaveReason(r));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

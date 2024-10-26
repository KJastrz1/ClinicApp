using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;

public sealed class LeaveStartDate : ValueObject
{
    private LeaveStartDate(DateTime value)
    {
        Value = value;
    }

    private LeaveStartDate() { }

    public DateTime Value { get; private set; }

    public static Result<LeaveStartDate> Create(DateTime startDate) =>
        Result.Create(startDate)
            .Ensure(
                date => date >= DateTime.UtcNow,
                EmployeeLeaveErrors.DateInThePast)
            .Map(date => new LeaveStartDate(date));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

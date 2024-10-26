using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;

public sealed class LeaveEndDate : ValueObject
{
    private LeaveEndDate(DateTime value)
    {
        Value = value;
    }

    private LeaveEndDate() { }

    public DateTime Value { get; private set; }

    public static Result<LeaveEndDate> Create(DateTime endDate) =>
        Result.Create(endDate)
            .Ensure(
                date=> date >= DateTime.UtcNow,
                EmployeeLeaveErrors.DateInThePast)
            .Map(date => new LeaveEndDate(date));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

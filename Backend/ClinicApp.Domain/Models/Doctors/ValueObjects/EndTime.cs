using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class EndTime : ValueObject
{
    public TimeSpan Value { get; }

    private EndTime(TimeSpan value)
    {
        Value = value;
    }

    public static Result<EndTime> Create(TimeSpan endTime)
    {
        return Result.Create(endTime)
            .Map(time => new EndTime(time));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

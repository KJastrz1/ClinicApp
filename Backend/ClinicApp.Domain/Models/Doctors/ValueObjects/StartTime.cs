using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class StartTime : ValueObject
{
    public TimeSpan Value { get; }

    private StartTime(TimeSpan value)
    {
        Value = value;
    }

    public static Result<StartTime> Create(TimeSpan value)
    {
        return Result.Create(value)
            .Map(time => new StartTime(time));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

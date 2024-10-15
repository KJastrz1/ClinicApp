using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class VisitDuration : ValueObject
{
    public static readonly TimeSpan MinDuration = TimeSpan.FromMinutes(1);
    public static readonly TimeSpan MaxDuration = TimeSpan.FromMinutes(120);

    public TimeSpan Value { get; }

    private VisitDuration(TimeSpan value)
    {
        Value = value;
    }

    public static Result<VisitDuration> Create(TimeSpan value)
    {
        return Result.Create(value)
            .Ensure(duration => duration >= MinDuration && duration <= MaxDuration, ScheduleErrors.InvalidVisitDuration)
            .Map(duration => new VisitDuration(duration));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

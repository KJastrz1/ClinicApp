using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class ScheduleVisitDuration : ValueObject
{
    public const int MinDurationMinutes = 1;
    public const int MaxDurationMinutes = 120;

    public int Value { get; }

    private ScheduleVisitDuration(int value)
    {
        Value = value;
    }

    public static Result<ScheduleVisitDuration> Create(int value)
    {
        return Result.Create(value)
            .Ensure(duration => duration >= MinDurationMinutes && duration <= MaxDurationMinutes, ScheduleErrors.InvalidVisitDuration)
            .Map(duration => new ScheduleVisitDuration(duration));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

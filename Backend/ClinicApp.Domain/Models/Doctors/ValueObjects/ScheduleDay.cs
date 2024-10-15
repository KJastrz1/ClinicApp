using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public sealed class ScheduleDay : ValueObject
{
    public DayOfWeek Value { get; }

    private ScheduleDay(DayOfWeek value)
    {
        Value = value;
    }

    public static Result<ScheduleDay> Create(DayOfWeek value)
    {
        return Result.Success(new ScheduleDay(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

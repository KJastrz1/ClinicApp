using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public sealed class AppointmentDurationMinutes : ValueObject
{
    public int Value { get; private set; }

    private AppointmentDurationMinutes(int minutes) => Value = minutes;

    public static Result<AppointmentDurationMinutes> Create(int minutes) =>
        Result.Create(minutes)
            .Ensure(
                m => m > ScheduleVisitDuration.MinDurationMinutes && m < ScheduleVisitDuration.MaxDurationMinutes, 
                AppointmentErrors.InvalidDuration)
            .Map(m => new AppointmentDurationMinutes(m));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public sealed class AppointmentStartDateTime : ValueObject
{
    private AppointmentStartDateTime(DateTime value) => Value = value;

    private AppointmentStartDateTime() { }

    public DateTime Value { get; private set; }

    public static Result<AppointmentStartDateTime> Create(DateTime dateTime) =>
        Result.Create(dateTime)
            .Ensure(
                d => d > DateTime.UtcNow,
                AppointmentErrors.AppointmentInPast)
            .Map(d => new AppointmentStartDateTime(d));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

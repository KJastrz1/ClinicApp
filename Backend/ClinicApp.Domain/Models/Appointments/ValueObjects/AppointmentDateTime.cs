using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public sealed class AppointmentDateTime : ValueObject
{
    private AppointmentDateTime(DateTime value) => Value = value;

    private AppointmentDateTime() { }

    public DateTime Value { get; private set; }

    public static Result<AppointmentDateTime> Create(DateTime dateTime) =>
        Result.Create(dateTime)
            .Ensure(
                d => d > DateTime.UtcNow,
                AppointmentErrors.AppointmentInPast)
            .Map(d => new AppointmentDateTime(d));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

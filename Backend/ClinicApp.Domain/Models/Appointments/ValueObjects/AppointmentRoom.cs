using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public sealed class AppointmentRoom : ValueObject
{
    public const int MaxLength = 50;

    private AppointmentRoom(string value) => Value = value;

    private AppointmentRoom() { }

    public string Value { get; private set; }

    public static Result<AppointmentRoom> Create(string room) =>
        Result.Create(room)
            .Ensure(
                r => !string.IsNullOrWhiteSpace(r),
                AppointmentErrors.RoomErrors.Required)
            .Ensure(
                r => r.Length <= MaxLength,
                AppointmentErrors.RoomErrors.TooLong)
            .Map(r => new AppointmentRoom(r));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

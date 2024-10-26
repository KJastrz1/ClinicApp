using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public sealed class AppointmentNotes : ValueObject
{
    public const int MaxLength = 1000;

    private AppointmentNotes(string value) => Value = value;

    private AppointmentNotes() { }

    public string Value { get; private set; }

    public static Result<AppointmentNotes> Create(string notes) =>
        Result.Create(notes)
            .Ensure(
                n => n.Length <= MaxLength,
                AppointmentErrors.NotesErrors.TooLong)
            .Ensure(n => !string.IsNullOrWhiteSpace(n), AppointmentErrors.NotesErrors.Empty)
            .Map(n => new AppointmentNotes(n));

    public static AppointmentNotes Empty => new(string.Empty);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

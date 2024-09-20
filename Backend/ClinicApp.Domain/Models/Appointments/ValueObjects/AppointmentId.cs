using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public class AppointmentId : ValueObject
{
    public Guid Value { get; }

    private AppointmentId(Guid value)
    {
        Value = value;
    }

    public static Result<AppointmentId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, AppointmentErrors.EmptyId)
            .Map(id => new AppointmentId(id));
    }

    public static AppointmentId New() => new(Guid.NewGuid());

    public static implicit operator AppointmentId(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

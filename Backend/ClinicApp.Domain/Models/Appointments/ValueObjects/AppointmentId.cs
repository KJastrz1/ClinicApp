using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Appointments.ValueObjects;

public class AppointmentId : ValueObject
{
    public Guid Value { get; }
    private AppointmentId(Guid value)
    {
        Value = value;
    }

    public static AppointmentId New() => new(Guid.NewGuid());
    
    public static implicit operator AppointmentId(Guid value) => new(value);

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

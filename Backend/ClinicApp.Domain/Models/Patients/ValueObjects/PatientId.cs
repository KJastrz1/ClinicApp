using ClinicApp.Domain.Models.User.ValueObjects;

namespace ClinicApp.Domain.Models.Patients.ValueObjects;

public class PatientId : UserId
{
    public PatientId(Guid value) : base(Validate(value)) { }

    public static PatientId New() => new(Guid.NewGuid());
    
    public static implicit operator PatientId(Guid value) => new(value);

    private static Guid Validate(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Patient ID cannot be an empty GUID.", nameof(value));
        }
        return value;
    }
}

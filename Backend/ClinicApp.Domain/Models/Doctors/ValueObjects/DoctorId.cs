using ClinicApp.Domain.Models.User.ValueObjects;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public class DoctorId : UserId
{
    public DoctorId(Guid value) : base(Validate(value)) { }

    public static DoctorId New() => new(Guid.NewGuid());
    
    public static implicit operator DoctorId(Guid value) => new(value);

    private static Guid Validate(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Doctor ID cannot be an empty GUID.", nameof(value));
        }

        return value;
    }
}

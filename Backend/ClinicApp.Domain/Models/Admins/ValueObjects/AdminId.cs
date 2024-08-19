using ClinicApp.Domain.Models.User.ValueObjects;

namespace ClinicApp.Domain.Models.Admins.ValueObjects;

public class AdminId : UserId
{
    public AdminId(Guid value) : base(Validate(value)) { }

    public static AdminId New() => new(Guid.NewGuid());
    
    public static implicit operator AdminId(Guid value) => new(value);

    private static Guid Validate(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Admin ID cannot be an empty GUID.", nameof(value));
        }

        return value;
    }
}

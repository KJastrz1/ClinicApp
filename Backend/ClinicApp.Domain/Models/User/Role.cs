using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.User;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Admin = new(1, nameof(Admin));
    public static readonly Role Doctor = new(2, nameof(Doctor));
    public static readonly Role Patient = new(3, nameof(Patient));

    public Role(int id, string name)
        : base(id, name)
    {
    }

    public ICollection<Permission> Permissions { get; set; }
    public ICollection<UserBase<UserId>> Users { get; set; }
}

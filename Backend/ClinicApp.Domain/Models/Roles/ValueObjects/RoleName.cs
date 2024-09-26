using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Roles.ValueObjects;

public sealed class RoleName : ValueObject
{
    public const int MaxLength = 100; 
    
    private RoleName(string value) => Value = value;

    private RoleName() { }

    public string Value { get; private set; }

    public static Result<RoleName> Create(string roleName) =>
        Result.Create(roleName)
            .Ensure(
                name => !string.IsNullOrWhiteSpace(name),
                RoleErrors.RoleNameErrors.Empty)
            .Ensure(
                name => name.Length <= MaxLength,
                RoleErrors.RoleNameErrors.TooLong)
            .Map(name => new RoleName(name));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

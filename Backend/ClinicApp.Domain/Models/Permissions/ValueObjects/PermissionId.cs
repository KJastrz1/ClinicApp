using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Permissions.ValueObjects;

public sealed class PermissionId : ValueObject
{
    public int Value { get; }

    private PermissionId(int value)
    {
        Value = value;
    }
    
    public static Result<PermissionId> Create(int value)
    {
        return Result.Create(value)
            .Ensure(id => id > 0, PermissionErrors.InvalidId)  
            .Map(id => new PermissionId(id));
    }

    public static implicit operator int(PermissionId permissionId) => permissionId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

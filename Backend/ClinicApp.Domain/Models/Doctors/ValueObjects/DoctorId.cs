using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public class DoctorId : ValueObject
{
    public Guid Value { get; }

    private DoctorId(Guid value)
    {
        Value = value;
    }

    public static Result<DoctorId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, UserBaseErrors.EmptyId)
            .Map(id => new DoctorId(id));
    }

    public static DoctorId New() => new(Guid.NewGuid());

    public static implicit operator Guid(DoctorId doctorId) => doctorId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients.ValueObjects;

public class PatientId : ValueObject
{
    public Guid Value { get; }

    private PatientId(Guid value)
    {
        Value = value;
    }

    public static Result<PatientId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, UserProfileErrors.EmptyId)
            .Map(id => new PatientId(id));
    }

    public static PatientId New() => new(Guid.NewGuid());

    public static implicit operator Guid(PatientId patientId) => patientId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

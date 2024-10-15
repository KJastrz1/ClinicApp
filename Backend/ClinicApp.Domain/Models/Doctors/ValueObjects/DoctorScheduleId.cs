using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Doctors.ValueObjects;

public class DoctorScheduleId : ValueObject
{
    public int Value { get; }

    private DoctorScheduleId(int value)
    {
        Value = value;
    }

    public static Result<DoctorScheduleId> Create(int value)
    {
        return Result.Create(value)
            .Ensure(id => id >0, ScheduleErrors.InvalidScheduleId)
            .Map(id => new DoctorScheduleId(id));
    }

    public static implicit operator int(DoctorScheduleId doctorScheduleId) => doctorScheduleId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

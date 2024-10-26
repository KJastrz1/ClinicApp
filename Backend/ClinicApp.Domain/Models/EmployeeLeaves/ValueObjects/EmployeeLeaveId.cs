using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.EmployeeLeaves.ValueObjects;

public class EmployeeLeaveId : ValueObject
{
    public Guid Value { get; }

    private EmployeeLeaveId(Guid value)
    {
        Value = value;
    }

    public static Result<EmployeeLeaveId> Create(Guid value)
    {
        return Result.Create(value)
            .Ensure(id => id != Guid.Empty, EmployeeLeaveErrors.EmptyId)
            .Map(id => new EmployeeLeaveId(id));
    }

    public static EmployeeLeaveId New() => new(Guid.NewGuid());

    public static implicit operator Guid(EmployeeLeaveId employeeLeaveId) => employeeLeaveId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

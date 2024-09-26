using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients.ValueObjects;

public class DateOfBirth : ValueObject
{
    public static readonly DateOnly MinDate = new(1900, 1, 1);
    public DateOnly Value { get; }

    private DateOfBirth(DateOnly value) => Value = value;

    public static Result<DateOfBirth> Create(DateOnly date)
    {
        return Result.Create(date)
            .Ensure(
                d => d <= DateOnly.FromDateTime(DateTime.UtcNow),
                PatientErrors.DateOfBirthErrors.InvalidFutureDate)
            .Ensure(
                d => d >= MinDate,
                PatientErrors.DateOfBirthErrors.InvalidPastDate)
            .Map(d => new DateOfBirth(d));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Patients.ValueObjects;

public class DateOfBirth : ValueObject
{
    public static readonly int MinYear = 1900;
    public DateTime Value { get; }

    private DateOfBirth(DateTime value) => Value = value;

    public static Result<DateOfBirth> Create(DateTime date) =>
        Result.Create(date)
            .Ensure(
                d => d <= DateTime.UtcNow,
                PatientErrors.DateOfBirthErrors.InvalidFutureDate)
            .Ensure(
                d => d.Year >= MinYear,
                PatientErrors.DateOfBirthErrors.InvalidPastDate)
            .Map(d => new DateOfBirth(d));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

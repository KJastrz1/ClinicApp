using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.UserProfiles.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;

    private FirstName(string value)
    {
        Value = value;
    }

    private FirstName()
    {
    }

    public string Value { get; private set; }

    public static Result<FirstName> Create(string firstName) =>
        Result.Create(firstName)
            .Ensure(
                f => !string.IsNullOrWhiteSpace(f),
                UserProfileErrors.FirstNameErrors.Empty)
            .Ensure(
                f => f.Length <= MaxLength,
                UserProfileErrors.FirstNameErrors.TooLong)
            .Map(f => new FirstName(f));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.User.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 50;

    private LastName(string value)
    {
        Value = value;
    }

    private LastName()
    {
    }

    public string Value { get; private set; }

    public static Result<LastName> Create(string lastName) =>
        Result.Create(lastName)
            .Ensure(
                l => !string.IsNullOrWhiteSpace(l),
                UserBaseErrors.LastNameErrors.Empty)
            .Ensure(
                l => l.Length <= MaxLength,
                UserBaseErrors.LastNameErrors.TooLong)
            .Map(l => new LastName(l));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

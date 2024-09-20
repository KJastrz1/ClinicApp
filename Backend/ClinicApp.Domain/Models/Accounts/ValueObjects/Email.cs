using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Accounts.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 255;

    private Email(string value) => Value = value;

    private Email()
    {
    }

    public string Value { get; private set; }

    public static Result<Email> Create(string email) =>
        Result.Create(email)
            .Ensure(
                e => !string.IsNullOrWhiteSpace(e),
                AccountErrors.EmailErrors.Empty)
            .Ensure(
                e => e.Length <= MaxLength,
                AccountErrors.EmailErrors.TooLong)
            .Ensure(
                e => e.Split('@').Length == 2,
                AccountErrors.EmailErrors.InvalidFormat)
            .Map(e => new Email(e));

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

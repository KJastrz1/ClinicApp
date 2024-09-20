using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Accounts.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public const int SaltLength = 32; 
    public const int HashLength = 64;

    private PasswordHash(string value)
    {
        Value = value;
    }

    private PasswordHash() { }

    public string Value { get; private set; } 

    public static Result<PasswordHash> Create(string passwordHash)
    {
        return Result.Create(passwordHash)
            .Ensure(
                ph => !string.IsNullOrWhiteSpace(ph),
                AccountErrors.PasswordHashErrors.Empty)
            .Ensure(
                ph => IsValidFormat(ph),
                AccountErrors.PasswordHashErrors.InvalidFormat)
            .Map(ph => new PasswordHash(ph));
    }

 
    private static bool IsValidFormat(string passwordHash)
    {
        string[] parts = passwordHash.Split('-');
        if (parts.Length != 2)
        {
            return false;
        }
        string salt = parts[0];
        string hash = parts[1];

        return salt.Length == SaltLength && hash.Length == HashLength;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

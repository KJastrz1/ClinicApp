using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Primitives;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Models.Accounts.ValueObjects;

public sealed class AccountId : ValueObject
{
    public Guid Value { get; }

    private AccountId(Guid value)
    {
        Value = value;
    }

    private AccountId() { }

    public static Result<AccountId> Create(Guid accountId)
    {
        return Result.Create(accountId)
            .Ensure(id => id != Guid.Empty, AccountErrors.EmptyId)
            .Map(id => new AccountId(id));
    }

    public static AccountId New() => new(Guid.NewGuid());

    public static implicit operator Guid(AccountId accountId) => accountId.Value;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

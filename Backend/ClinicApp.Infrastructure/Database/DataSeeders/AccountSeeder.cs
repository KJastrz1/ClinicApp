using ClinicApp.Application.Actions.Accounts.Command.RegisterAccount;
using ClinicApp.Domain.Shared;
using MediatR;

namespace ClinicApp.Infrastructure.Database.DataSeeders;
public class AccountSeeder :IDataSeeder
{
    private readonly ISender _sender;

    public AccountSeeder(ISender sender)
    {
        _sender = sender;
    }

    public async Task SeedAsync()
    {
        var accounts = new List<RegisterAccountCommand>
        {
            new RegisterAccountCommand("patient@email.com", "Password1!"),
            new RegisterAccountCommand("doctor@email.com", "Password2!"),
            new RegisterAccountCommand("admin@email.com", "Password3!")
        };

        foreach (RegisterAccountCommand account in accounts)
        {
            Result<Guid> result = await _sender.Send(account);

            if (result.IsFailure)
            {
                Console.WriteLine($"Error registering {account.Email}: {result.Error}");
            }
        }
    }
}

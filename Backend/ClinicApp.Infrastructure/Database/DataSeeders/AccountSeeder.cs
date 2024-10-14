using ClinicApp.Application.Actions.Accounts.Command.RegisterAccount;
using ClinicApp.Application.Actions.Accounts.Command.AddRolesToAccount;
using ClinicApp.Domain.Shared;
using MediatR;
using ClinicApp.Domain.Enums;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public class AccountSeeder : IDataSeeder
{
    private readonly ISender _sender;

    public AccountSeeder(ISender sender)
    {
        _sender = sender;
    }

    public async Task SeedAsync()
    {
        var accounts = new List<(RegisterAccountCommand command, Guid roleId)>
        {
            (new RegisterAccountCommand("patient@email.com", "Password1!"), BasicRoles.Patient.Id),
            (new RegisterAccountCommand("doctor@email.com", "Password2!"), BasicRoles.Doctor.Id),
            (new RegisterAccountCommand("admin@email.com", "Password3!"), BasicRoles.Admin.Id)
        };

        foreach (var (accountCommand, roleId) in accounts)
        {
  
            Result<Guid> accountResult = await _sender.Send(accountCommand);

            if (accountResult.IsFailure)
            {
                Console.WriteLine($"Error registering {accountCommand.Email}: {accountResult.Error}");
                continue;
            }

            Guid accountId = accountResult.Value;
     
            var addRoleCommand = new AddRolesToAccountCommand(accountId, new List<Guid> { roleId });
            Result<Guid> roleResult = await _sender.Send(addRoleCommand);

            if (roleResult.IsFailure)
            {
                Console.WriteLine($"Error adding role {BasicRoles.FromId(roleId)} to {accountCommand.Email}: {roleResult.Error}");
            }
        }
    }
}

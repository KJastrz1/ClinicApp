using ClinicApp.Application.UseCases.Accounts.Command.AddRolesToAccount;
using ClinicApp.Application.UseCases.Accounts.Command.RemoveRolesFromAccount;
using ClinicApp.Application.UseCases.Accounts.Query.GetAccountById;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Account.Requests;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/accounts")]
public sealed class AccountsController : ApiController
{
    public AccountsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccountById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAccountByIdQuery(id);

        Result<AccountResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpPatch("{id:guid}/add-roles")]
    public async Task<IActionResult> AddRolesToAccount(
        Guid id,
        [FromBody] AddRolesToAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddRolesToAccountCommand(id, request.RolesIds);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpPatch("{id:guid}/remove-roles")]
    public async Task<IActionResult> RemoveRolesFromAccount(
        Guid id,
        [FromBody] RemoveRolesFromAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RemoveRolesFromAccountCommand(id, request.RolesIds);

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}

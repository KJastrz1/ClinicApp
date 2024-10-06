using ClinicApp.Application.Actions.Accounts.Command.RegisterAccount;
using ClinicApp.Application.Actions.Accounts.Query.GetAccountById;
using ClinicApp.Application.Actions.Accounts.Query.LoginAccount;
using ClinicApp.Domain.Shared;
using ClinicApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Account.Requests;
using Shared.Contracts.Account.Responses;

namespace ClinicApp.Presentation.Controllers;

[Route("api/auth")]
public sealed class AuthController : ApiController
{
    public AuthController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAccount(
        [FromBody] RegisterAccountRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterAccountCommand(
            request.Email,
            request.Password
        );

        Result<Guid> result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(
            nameof(LoginAccount),
            new { id = result.Value },
            result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAccount(
        [FromBody] LoginAccountRequest request,
        CancellationToken cancellationToken)
    {
        var query = new LoginAccountQuery(
            request.Email,
            request.Password
        );

        Result<LoginResponse> result = await Sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }
}

using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.UseCases.Users.Query.GetUserById;
using ClinicApp.Domain.Shared;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Auth.Responses;


namespace ClinicApp.Presentation.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly SignInManager<User> _signInManager;

    public UsersController(ISender sender, SignInManager<User> signInManager)
    {
        _signInManager = signInManager;

        _sender = sender;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        string? userId = HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
        {
            return Unauthorized("User is not authenticated");
        }

        var query = new GetUserByIdQuery(parsedUserId);

        Result<UserResponse> result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}

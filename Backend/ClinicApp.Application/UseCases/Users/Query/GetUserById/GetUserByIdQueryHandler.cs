using ClinicApp.Application.Abstractions.Authentication;
using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;
using Shared.Contracts.Auth.Responses;

namespace ClinicApp.Application.UseCases.Users.Query.GetUserById;

internal sealed class GetUserByIdQueryHandler
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserContext _userContext;

    public GetUserByIdQueryHandler(IUserContext userContext)
    {
        _userContext = userContext;
    }

    public async Task<Result<UserResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        UserId? userId = _userContext.Id;

        if (userId == null || userId.Value != request.UserId)
        {
            return Result.Failure<UserResponse>(UserProfileErrors.NotFound(userId));
        }

        IList<string> roles = await _userContext.GetRolesAsync();

        var userResponse = new UserResponse(
            userId.Value,
            _userContext.Email,
            roles.ToList(),
            _userContext.IsEmailConfirmed
        );

        return userResponse;
    }
}

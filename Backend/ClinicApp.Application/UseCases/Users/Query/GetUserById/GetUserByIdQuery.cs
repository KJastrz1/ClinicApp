using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Auth.Responses;

namespace ClinicApp.Application.UseCases.Users.Query.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;

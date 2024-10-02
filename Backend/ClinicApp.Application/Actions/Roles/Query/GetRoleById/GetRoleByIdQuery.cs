using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Domain.Shared;
using MediatR;
using Shared.Contracts.Account.Responses;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetRoleById;

public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleResponse>;

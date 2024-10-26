using ClinicApp.Application.Abstractions.Messaging;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.UseCases.Roles.Query.GetRoleById;

public sealed record GetRoleByIdQuery(Guid Id) : IQuery<RoleResponse>;

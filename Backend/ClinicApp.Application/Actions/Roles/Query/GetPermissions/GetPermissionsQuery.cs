using ClinicApp.Domain.Shared;
using MediatR;
using Shared.Contracts.Account.Responses;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Application.Actions.Roles.Query.GetPermissions;

public sealed record GetPermissionsQuery() : IRequest<Result<List<PermissionResponse>>>;

using System.Text;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using Dapper;
using Npgsql;
using Shared.Contracts;
using Shared.Contracts.Role.Requests;
using Shared.Contracts.Role.Responses;

namespace ClinicApp.Infrastructure.Database.Repositories.Dapper;

public class RoleReadDapperRepository : IRoleReadDapperRepository
{
    private readonly string _connectionString;

    public RoleReadDapperRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<RoleResponse?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $@"
    SELECT r.""Id"", r.""Name""
    FROM ""{TableNames.Roles}"" r
    WHERE r.""Id"" = @RoleId";

        var parameters = new { RoleId = roleId.Value };

        RoleReadModel? roleReadModel = await connection.QuerySingleOrDefaultAsync<RoleReadModel>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        if (roleReadModel != null)
        {
            string permissionsQuery = $@"
        SELECT p.""Id"", p.""Name""
        FROM ""{TableNames.RolePermissions}"" rp
        JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
        WHERE rp.""RoleId"" = @RoleId";

            IEnumerable<PermissionReadModel> permissionReadModels = await connection.QueryAsync<PermissionReadModel>(
                new CommandDefinition(permissionsQuery, parameters, cancellationToken: cancellationToken));

            roleReadModel.Permissions = permissionReadModels.ToList();

            return roleReadModel.ToResponse();
        }

        return null;
    }

    public async Task<PagedItems<RoleResponse>> GetByFilterAsync(
        RoleFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var queryBuilder = new StringBuilder($@"
SELECT r.""Id"", r.""Name""
FROM ""{TableNames.Roles}"" r");

        var parameters = new DynamicParameters();
        bool whereAdded = false;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            queryBuilder.Append(@" WHERE r.""Name"" ILIKE @Name");
            parameters.Add("@Name", $"%{filter.Name}%");
            whereAdded = true;
        }

        if (!string.IsNullOrEmpty(filter.PermissionName))
        {
            queryBuilder.Append(whereAdded ? @" AND" : @" WHERE");
            whereAdded = true;

            queryBuilder.Append($@"
    EXISTS (
        SELECT 1
        FROM ""{TableNames.RolePermissions}"" rp
        JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
        WHERE rp.""RoleId"" = r.""Id"" AND p.""Name"" ILIKE @PermissionName)");

            parameters.Add("@PermissionName", $"%{filter.PermissionName}%");
        }

        string countQuery = $"SELECT COUNT(*) FROM ({queryBuilder}) AS CountQuery";

        int totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countQuery, parameters, cancellationToken: cancellationToken));

        queryBuilder.Append(@" ORDER BY r.""Id"" DESC LIMIT @PageSize OFFSET @Offset");
        parameters.Add("@Offset", (pageNumber - 1) * pageSize);
        parameters.Add("@PageSize", pageSize);

        IEnumerable<RoleReadModel> roles = await connection.QueryAsync<RoleReadModel>(
            new CommandDefinition(queryBuilder.ToString(), parameters, cancellationToken: cancellationToken));

        var rolesWithPermissions = new List<RoleReadModel>();
        foreach (RoleReadModel role in roles)
        {
            string permissionsQuery = $@"
    SELECT p.""Id"", p.""Name""
    FROM ""{TableNames.RolePermissions}"" rp
    JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
    WHERE rp.""RoleId"" = @RoleId";

            IEnumerable<PermissionReadModel> permissions = await connection.QueryAsync<PermissionReadModel>(
                new CommandDefinition(permissionsQuery, new { RoleId = role.Id },
                    cancellationToken: cancellationToken));

            role.Permissions = permissions.ToList();
            rolesWithPermissions.Add(role);
        }

        return new PagedItems<RoleResponse>
        {
            Items = rolesWithPermissions.Select(r => r.ToResponse()).ToList(),
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}

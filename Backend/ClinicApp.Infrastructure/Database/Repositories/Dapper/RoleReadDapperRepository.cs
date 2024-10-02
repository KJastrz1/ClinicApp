using System.Text;
using ClinicApp.Application.ReadRepositories;
using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Models.Roles.ValueObjects;
using ClinicApp.Infrastructure.Database.Constants;
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

        RoleResponse? role = await connection.QuerySingleOrDefaultAsync<RoleResponse>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        if (role != null)
        {
            string permissionsQuery = $@"
            SELECT p.""Id"", p.""Name""
            FROM ""{TableNames.RolePermissions}"" rp
            JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
            WHERE rp.""RoleId"" = @RoleId";

            IEnumerable<PermissionResponse> permissions = await connection.QueryAsync<PermissionResponse>(
                new CommandDefinition(permissionsQuery, parameters, cancellationToken: cancellationToken));


            return role with { Permissions = permissions.ToList() };
        }

        return null;
    }


    public async Task<PagedResult<RoleResponse>> GetByFilterAsync(
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

        if (!string.IsNullOrEmpty(filter.Name))
        {
            queryBuilder.Append(@" WHERE r.""Name"" ILIKE @Name");
            parameters.Add("@Name", $"%{filter.Name}%");
        }

        if (!string.IsNullOrEmpty(filter.PermissionName))
        {
            queryBuilder.Append(@"
            JOIN ""{TableNames.RolePermissions}"" rp ON rp.""RoleId"" = r.""Id""
            JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
            WHERE p.""Name"" ILIKE @PermissionName");

            parameters.Add("@PermissionName", $"%{filter.PermissionName}%");
        }

        string countQuery = $@"SELECT COUNT(DISTINCT r.""Id"") FROM ({{queryBuilder}}) AS CountQuery";

        int totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countQuery, parameters, cancellationToken: cancellationToken));

        queryBuilder.Append(@" ORDER BY r.""Id"" DESC LIMIT @PageSize OFFSET @Offset");
        parameters.Add("@Offset", (pageNumber - 1) * pageSize);
        parameters.Add("@PageSize", pageSize);

        IEnumerable<RoleResponse> items = await connection.QueryAsync<RoleResponse>(
            new CommandDefinition(queryBuilder.ToString(), parameters, cancellationToken: cancellationToken));


        var rolesWithPermissions = new List<RoleResponse>();
        foreach (RoleResponse role in items)
        {
            string permissionsQuery = $@"
            SELECT p.""Id"", p.""Name""
            FROM ""{TableNames.RolePermissions}"" rp
            JOIN ""{TableNames.Permissions}"" p ON p.""Id"" = rp.""PermissionId""
            WHERE rp.""RoleId"" = @RoleId";

            IEnumerable<PermissionResponse> permissions = await connection.QueryAsync<PermissionResponse>(
                new CommandDefinition(permissionsQuery, new { RoleId = role.Id },
                    cancellationToken: cancellationToken));

            rolesWithPermissions.Add(role with
            {
                Permissions = permissions.ToList()
            });
        }

        return new PagedResult<RoleResponse>
        {
            Items = rolesWithPermissions,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}

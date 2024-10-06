using ClinicApp.Application.ReadRepositories.Dapper;
using ClinicApp.Domain.Models.Roles;
using ClinicApp.Infrastructure.Database.Constants;
using Dapper;
using Npgsql;
using Shared.Contracts.Role.Requests;
using System.Text;
using ClinicApp.Domain.Models.Permissions;


namespace ClinicApp.Infrastructure.Database.Repositories.Dapper;

public class PermissionReadDapperRepository : IPermissionReadDapperRepository
{
    private readonly string _connectionString;

    public PermissionReadDapperRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Permission?> GetByIdAsync(int permissionId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $@"
                SELECT p.""Id"", p.""Name""
                FROM ""{TableNames.Permissions}"" p
                WHERE p.""Id"" = @PermissionId";

        var parameters = new { PermissionId = permissionId };

        Permission? permission = await connection.QuerySingleOrDefaultAsync<Permission>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        return permission;
    }

    public async Task<List<Permission>> GetByFilterAsync(
        PermissionFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var queryBuilder = new StringBuilder($@"
                SELECT p.""Id"", p.""Name""
                FROM ""{TableNames.Permissions}"" p
                WHERE 1=1");

        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            queryBuilder.Append(@" AND p.""Name"" ILIKE @Name");
            parameters.Add("@Name", $"%{filter.Name}%");
        }

        string countQuery = $"SELECT COUNT(*) FROM ({queryBuilder}) AS CountQuery";
        int totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countQuery, parameters, cancellationToken: cancellationToken));

        queryBuilder.Append(@" ORDER BY p.""Id"" LIMIT @PageSize OFFSET @Offset");
        parameters.Add("@Offset", (pageNumber - 1) * pageSize);
        parameters.Add("@PageSize", pageSize);

        IEnumerable<Permission> permissions = await connection.QueryAsync<Permission>(
            new CommandDefinition(queryBuilder.ToString(), parameters, cancellationToken: cancellationToken));

        return permissions.ToList();
    }
}

using System.Text;
using ClinicApp.Domain.Models.Accounts.ValueObjects;
using ClinicApp.Domain.Repositories.Read;
using ClinicApp.Infrastructure.Database.ReadModels.Auth;
using ClinicApp.Infrastructure.Database.Constants;
using Dapper;
using Npgsql;
using Shared.Contracts;
using Shared.Contracts.Auth;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class AccountReadRepository : IAccountReadRepository
{
    private readonly string _connectionString;

    public AccountReadRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<AccountResponse?> GetByIdAsync(AccountId accountId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $@"
        SELECT ""Id"", ""Email"", ""IsActivated"", ""CreatedOnUtc"", ""ModifiedOnUtc""
        FROM ""{TableNames.Accounts}""
        WHERE ""Id"" = @AccountId";

        var parameters = new { AccountId = accountId.Value };

        AccountReadModel? accountReadModel = await connection.QuerySingleOrDefaultAsync<AccountReadModel>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        if (accountReadModel == null)
        {
            return null;
        }

        string rolesQuery = $@"
        SELECT r.""Name"", p.""Name"" AS PermissionName
        FROM ""{TableNames.AccountRoles}"" ar
        JOIN ""{TableNames.Roles}"" r ON ar.""RoleId"" = r.""Id""
        LEFT JOIN ""{TableNames.RolePermissions}"" rp ON r.""Id"" = rp.""RoleId""
        LEFT JOIN ""{TableNames.Permissions}"" p ON rp.""PermissionId"" = p.""Id""
        WHERE ar.""AccountId"" = @AccountId";

        var roleLookup = new Dictionary<string, RoleReadModel>();

        await connection.QueryAsync<RoleReadModel, PermissionReadModel, RoleReadModel>(
            new CommandDefinition(rolesQuery, new { AccountId = accountId.Value },
                cancellationToken: cancellationToken),
            (role, permission) =>
            {
                if (!roleLookup.TryGetValue(role.Name, out RoleReadModel? roleEntry))
                {
                    roleEntry = new RoleReadModel
                    {
                        Name = role.Name,
                        Permissions = new List<PermissionReadModel>()
                    };
                    roleLookup.Add(role.Name, roleEntry);
                }

                if (permission != null && !roleEntry.Permissions.Any(p => p.Name == permission.Name))
                {
                    roleEntry.Permissions.Add(permission);
                }

                return roleEntry;
            });

        accountReadModel.Roles = roleLookup.Values.ToList();

        return accountReadModel.MapToResponse();
    }

    public async Task<PagedResult<AccountResponse>> GetByFilterAsync(
        AccountFilter filter,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var queryBuilder = new StringBuilder($@"
            SELECT ""Id"", ""Email"", ""IsActivated"", ""CreatedOnUtc"", ""ModifiedOnUtc""
            FROM ""{TableNames.Accounts}""
            WHERE 1=1");

        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(filter.Email))
        {
            queryBuilder.Append(" AND Email ILIKE @Email");
            parameters.Add("@Email", $"%{filter.Email}%");
        }

        if (filter.IsActivated.HasValue)
        {
            queryBuilder.Append(" AND IsActivated = @IsActivated");
            parameters.Add("@IsActivated", filter.IsActivated.Value);
        }

        if (filter.CreatedOnUtcStart.HasValue)
        {
            queryBuilder.Append(" AND CreatedOnUtc >= @CreatedOnUtcStart");
            parameters.Add("@CreatedOnUtcStart", filter.CreatedOnUtcStart.Value);
        }

        if (filter.CreatedOnUtcEnd.HasValue)
        {
            queryBuilder.Append(" AND CreatedOnUtc <= @CreatedOnUtcEnd");
            parameters.Add("@CreatedOnUtcEnd", filter.CreatedOnUtcEnd.Value);
        }

        string countQuery = $"SELECT COUNT(*) FROM ({queryBuilder}) AS CountQuery";

        int totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countQuery, parameters, cancellationToken: cancellationToken));

        queryBuilder.Append(" ORDER BY \"CreatedOnUtc\" DESC LIMIT @PageSize OFFSET @Offset");
        parameters.Add("@Offset", (pageNumber - 1) * pageSize);
        parameters.Add("@PageSize", pageSize);

        IEnumerable<AccountReadModel> accountReadModels = await connection.QueryAsync<AccountReadModel>(
            new CommandDefinition(queryBuilder.ToString(), parameters, cancellationToken: cancellationToken));

        var accountResponses = accountReadModels.Select(a => a.MapToResponse()).ToList();

        return new PagedResult<AccountResponse>
        {
            Items = accountResponses,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}

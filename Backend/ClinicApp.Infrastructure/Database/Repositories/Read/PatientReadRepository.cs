using System.Text;
using ClinicApp.Domain.Models.Patients.ValueObjects;
using ClinicApp.Domain.Repositories;
using ClinicApp.Domain.Repositories.Read;
using ClinicApp.Infrastructure.Database.Constants;
using Dapper;
using Npgsql;
using Shared.Contracts;
using Shared.Contracts.Patient;

namespace ClinicApp.Infrastructure.Database.Repositories.Read;

public class PatientReadRepository : IPatientReadRepository
{
    private readonly string _connectionString;

    public PatientReadRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<PatientResponse?> GetByIdAsync(PatientId patientId, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        string query = $@"
        SELECT ""Id"", ""Email"", ""FirstName"", ""LastName"", ""SocialSecurityNumber"", ""DateOfBirth"", 
               ""IsActivated"", ""CreatedOnUtc"", ""ModifiedOnUtc""
        FROM ""{TableNames.Patients}""
        WHERE ""Id"" = @PatientId";

        var parameters = new { PatientId = patientId.Value };

        PatientResponse? patient = await connection.QuerySingleOrDefaultAsync<PatientResponse>(
            new CommandDefinition(query, parameters, cancellationToken: cancellationToken));

        return patient;
    }

    public async Task<PagedResult<PatientResponse>> GetByFilterAsync(
        PatientFilter filter, 
        int pageNumber, 
        int pageSize,
        CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var queryBuilder = new StringBuilder($@"
            SELECT ""Id"", ""Email"", ""FirstName"", ""LastName"", ""SocialSecurityNumber"", ""DateOfBirth"", 
                   ""IsActivated"", ""CreatedOnUtc"", ""ModifiedOnUtc""
            FROM ""{TableNames.Patients}""
            WHERE 1=1");

        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(filter.Email))
        {
            queryBuilder.Append(" AND Email ILIKE @Email");
            parameters.Add("@Email", $"%{filter.Email}%");
        }

        if (!string.IsNullOrEmpty(filter.FirstName))
        {
            queryBuilder.Append(" AND FirstName ILIKE @FirstName");
            parameters.Add("@FirstName", $"%{filter.FirstName}%");
        }

        if (!string.IsNullOrEmpty(filter.LastName))
        {
            queryBuilder.Append(" AND LastName ILIKE @LastName");
            parameters.Add("@LastName", $"%{filter.LastName}%");
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

        if (filter.ModifiedOnUtcStart.HasValue)
        {
            queryBuilder.Append(" AND ModifiedOnUtc >= @ModifiedOnUtcStart");
            parameters.Add("@ModifiedOnUtcStart", filter.ModifiedOnUtcStart.Value);
        }

        if (filter.ModifiedOnUtcEnd.HasValue)
        {
            queryBuilder.Append(" AND ModifiedOnUtc <= @ModifiedOnUtcEnd");
            parameters.Add("@ModifiedOnUtcEnd", filter.ModifiedOnUtcEnd.Value);
        }

        if (!string.IsNullOrEmpty(filter.SocialSecurityNumber))
        {
            queryBuilder.Append(" AND SocialSecurityNumber = @SocialSecurityNumber");
            parameters.Add("@SocialSecurityNumber", filter.SocialSecurityNumber);
        }

        if (filter.DateOfBirthStart.HasValue)
        {
            queryBuilder.Append(" AND DateOfBirth >= @DateOfBirthStart");
            parameters.Add("@DateOfBirthStart", filter.DateOfBirthStart.Value);
        }

        if (filter.DateOfBirthEnd.HasValue)
        {
            queryBuilder.Append(" AND DateOfBirth <= @DateOfBirthEnd");
            parameters.Add("@DateOfBirthEnd", filter.DateOfBirthEnd.Value);
        }

        string countQuery = $"SELECT COUNT(*) FROM ({queryBuilder}) AS CountQuery";

        int totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countQuery, parameters, cancellationToken: cancellationToken));

        queryBuilder.Append(" ORDER BY \"CreatedOnUtc\" DESC LIMIT @PageSize OFFSET @Offset");
        parameters.Add("@Offset", (pageNumber - 1) * pageSize);
        parameters.Add("@PageSize", pageSize);

        IEnumerable<PatientResponse> items = await connection.QueryAsync<PatientResponse>(
            new CommandDefinition(queryBuilder.ToString(), parameters, cancellationToken: cancellationToken));

        return new PagedResult<PatientResponse>
        {
            Items = items.ToList(),
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber
        };
    }
}

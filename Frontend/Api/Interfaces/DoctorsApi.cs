using System.Net.Http.Json;
using Shared.Contracts.Doctor.Requests;
using Shared.Contracts.Doctor.Responses;
using Shared.Contracts.Shared;

namespace Frontend.Api.Interfaces;

public class DoctorsApi : IDoctorsApi
{
    private readonly HttpClient _httpClient;

    public DoctorsApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PagedItems<DoctorBriefResponse>> GetDoctorsAsync(DoctorFilter filter, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var queryParameters = new Dictionary<string, string>
        {
            { "PageNumber", pageNumber.ToString() },
            { "PageSize", pageSize.ToString() }
        };

        if (!string.IsNullOrEmpty(filter?.FirstName))
        {
            queryParameters.Add("FirstName", filter.FirstName);
        }

        if (!string.IsNullOrEmpty(filter?.LastName))
        {
            queryParameters.Add("LastName", filter.LastName);
        }

        if (!string.IsNullOrEmpty(filter?.Specialty))
        {
            queryParameters.Add("Specialty", filter.Specialty);
        }

        string queryString = string.Join("&", queryParameters.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

        PagedItems<DoctorBriefResponse>? response = await _httpClient.GetFromJsonAsync<PagedItems<DoctorBriefResponse>>($"doctors?{queryString}", cancellationToken);

        return response ?? new PagedItems<DoctorBriefResponse>();
    }

}

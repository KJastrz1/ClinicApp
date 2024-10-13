using ClinicApp.Domain.Models.Clinics;
using Shared.Contracts;
using Shared.Contracts.Clinic;
using Shared.Contracts.Clinic.Responses;

namespace ClinicApp.Application.Mappings;

public static class ClinicMappings
{
    public static ClinicResponse ToResponse(this Clinic clinic)
    {
        return new ClinicResponse(
            Id: clinic.Id.Value,
            PhoneNumber: clinic.PhoneNumber.Value,
            Address: clinic.Address.Value,
            City: clinic.City.Value,
            ZipCode: clinic.ZipCode.Value,
            CreatedOnUtc: clinic.CreatedOnUtc,
            ModifiedOnUtc: clinic.ModifiedOnUtc
        );
    }
}

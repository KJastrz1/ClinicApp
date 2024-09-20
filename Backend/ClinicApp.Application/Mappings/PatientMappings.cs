using ClinicApp.Domain.Models.Patients;
using Shared.Contracts.Patient;

namespace ClinicApp.Application.Mappings;

public static class PatientMappings
{
    public static PatientResponse MapToResponse(this Patient entity)
    {
        return new PatientResponse(
            entity.Id.Value,
            entity.FirstName.Value,
            entity.LastName.Value,
            entity.SocialSecurityNumber.Value,
            entity.DateOfBirth.Value
        );
    }
}

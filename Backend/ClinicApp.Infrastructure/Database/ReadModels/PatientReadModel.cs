using Shared.Contracts.Patient;
using Shared.Contracts.Patient.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class PatientReadModel : UserReadModel
{
    public string SocialSecurityNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    internal PatientResponse MapToResponse()
    {
        return new PatientResponse(
            Id: Id,
            FirstName: FirstName,
            LastName: LastName,
            SocialSecurityNumber: SocialSecurityNumber,
            DateOfBirth: DateOfBirth
        );
    }
}

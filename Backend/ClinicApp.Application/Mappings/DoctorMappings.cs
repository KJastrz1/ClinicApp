using ClinicApp.Domain.Models.Doctors;
using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Application.Mappings;

public static class DoctorMappings
{
    public static DoctorResponse ToResponse(this Doctor doctor)
    {
        return new DoctorResponse(
            Id: doctor.Id.Value,
            FirstName: doctor.FirstName.Value,
            LastName: doctor.LastName.Value,
            MedicalLicenseNumber: doctor.MedicalLicenseNumber.Value,
            Specialties: doctor.Specialties.Select(s => s.Value).ToArray(),
            Bio: doctor.Bio?.Value,
            AcademicTitle: doctor.AcademicTitle?.Value,
            ClinicId: doctor.ClinicId?.Value ?? Guid.Empty
        );
    }
}

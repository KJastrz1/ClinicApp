using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class DoctorReadModel : EmployeeReadModel
{
    public string MedicalLicenseNumber { get; set; }
    public string? Bio { get; set; }
    public string? AcademicTitle { get; set; }
    public List<SpecialtyReadModel> Specialties { get; set; } = new List<SpecialtyReadModel>();
    public List<DoctorScheduleReadModel> Schedules { get; set; } = new List<DoctorScheduleReadModel>();

    internal new DoctorResponse MapToResponse()
    {
        return new DoctorResponse(
            Id: Id,
            FirstName: FirstName,
            LastName: LastName,
            MedicalLicenseNumber: MedicalLicenseNumber,
            Specialties: Specialties.Select(s => s.Value).ToList(),
            Bio: Bio,
            AcademicTitle: AcademicTitle
        );
    }

    internal DoctorBriefResponse MapToBriefResponse()
    {
        return new DoctorBriefResponse(
            Id: Id,
            FirstName: FirstName,
            LastName: LastName,
            MedicalLicenseNumber: MedicalLicenseNumber,
            Specialties: Specialties.Select(s => s.Value).ToList(),
            AcademicTitle: AcademicTitle
        );
    }
}

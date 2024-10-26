using Shared.Contracts.Doctor.Responses;

namespace ClinicApp.Infrastructure.Database.ReadModels;

internal class DoctorReadModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MedicalLicenseNumber { get; set; }
    public string? Bio { get; set; }
    public string? AcademicTitle { get; set; }
    public List<string> Specialties { get; set; } = new List<string>();
    public List<DoctorScheduleReadModel> Schedules { get; set; } = new List<DoctorScheduleReadModel>();
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    internal DoctorResponse MapToResponse()
    {
        return new DoctorResponse(
            Id: Id,
            FirstName: FirstName,
            LastName: LastName,
            MedicalLicenseNumber: MedicalLicenseNumber,
            Specialties: Specialties,
            Bio: Bio,
            AcademicTitle: AcademicTitle
        );
    }
}

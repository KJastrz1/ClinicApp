using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.Actions.Doctors.Command.UpdateDoctor;

public sealed record UpdateDoctorCommand(
    Guid DoctorId,
    string? FirstName,
    string? LastName,
    string? MedicalLicenseNumber,
    List<string>? Specialties,
    string? Bio,
    string? AcademicTitle,
    Guid? ClinicId
) : ICommand<Guid>;

using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Doctors.Command.UpdateDoctor;

public sealed record UpdateDoctorCommand(
    Guid DoctorId,
    string? FirstName,
    string? LastName,
    string? MedicalLicenseNumber,
    List<string>? Specialties,
    string? Bio,
    string? AcademicTitle
) : ICommand<Guid>;

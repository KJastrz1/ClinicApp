using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Doctors.Command.CreateDoctor;

public sealed record CreateDoctorCommand(
    string FirstName,
    string LastName,
    string MedicalLicenseNumber,
    List<string>? Specialties,
    string? Bio,
    string? AcademicTitle,
    Guid? AccountId
) : ICommand<Guid>;

using ClinicApp.Application.Abstractions.Messaging;

namespace ClinicApp.Application.UseCases.Patients.Command.DeletePatient;

public sealed record DeletePatientCommand(Guid PatientId) : ICommand<bool>;

using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;

namespace ClinicApp.Application.RepositoryInterfaces.Write;

public interface IAppointmentRepository
{
    Task<Appointment?> GetByIdAsync(
        AppointmentId id,
        CancellationToken cancellationToken);
    
    Task<Appointment?> GetByStartDate(
        AppointmentStartDateTime startDateTime,
        CancellationToken cancellationToken);
  
    void Add(Appointment appointment);

    void Update(Appointment appointment);

    void Remove(Appointment appointment);
}

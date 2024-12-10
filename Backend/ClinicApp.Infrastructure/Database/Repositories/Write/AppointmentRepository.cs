using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Models.Appointments;
using ClinicApp.Domain.Models.Appointments.ValueObjects;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Database.Repositories.Write;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly WriteDbContext _writeContext;

    public AppointmentRepository(WriteDbContext writeContext)
    {
        _writeContext = writeContext;
    }

    public async Task<Appointment?> GetByIdAsync(AppointmentId id, CancellationToken cancellationToken)
    {
        return await _writeContext.Appointments
            .FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
    }

    public Task<Appointment?> GetByStartDate(AppointmentStartDateTime startDateTime, CancellationToken cancellationToken)
    {
        return _writeContext.Appointments
            .FirstOrDefaultAsync(a => a.StartDateTime.Equals(startDateTime), cancellationToken);
    }

    public void Add(Appointment appointment)
    {
        _writeContext.Appointments.Add(appointment);
    }

    public void Update(Appointment appointment)
    {
        _writeContext.Appointments.Update(appointment);
    }

    public void Remove(Appointment appointment)
    {
        _writeContext.Appointments.Remove(appointment);
    }
}

using ClinicApp.Domain.Models.Doctors.ValueObjects;

namespace ClinicApp.Domain.Models.Doctors;

public class DoctorSpecialty
{
    public Specialty Specialty { get; private set; }

    public DoctorSpecialty(Specialty specialty)
    {
        Specialty = specialty;
    }
}

using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics;

public class Clinic : AggregateRoot<ClinicId>, IAuditableEntity
{
    public ClinicAddress Address { get; private set; }
    public ClinicPhoneNumber PhoneNumber { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public Clinic(
        ClinicId id,
        ClinicAddress address,
        ClinicPhoneNumber phoneNumber) : base(id)
    {
        Address = address;
        PhoneNumber = phoneNumber;
        CreatedOnUtc = DateTime.UtcNow;
    }

    private Clinic() { }

    public void UpdateAddress(ClinicAddress newAddress)
    {
        Address = newAddress;
        ModifiedOnUtc = DateTime.UtcNow;
    }

    public void UpdatePhoneNumber(ClinicPhoneNumber newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
        ModifiedOnUtc = DateTime.UtcNow;
    }
}

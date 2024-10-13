using ClinicApp.Domain.Models.Clinics.DomainEvents;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Primitives;

namespace ClinicApp.Domain.Models.Clinics;

public class Clinic : AggregateRoot<ClinicId>, IAuditableEntity
{
    public PhoneNumber PhoneNumber { get; private set; }
    public Address Address { get; private set; }
    public City City { get; private set; }
    public ZipCode ZipCode { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }

    private Clinic() { }

    private Clinic(
        ClinicId id,
        PhoneNumber phoneNumber,
        Address address,
        City city,
        ZipCode zipCode
    ) : base(id)
    {
        Address = address;
        City = city;
        ZipCode = zipCode;
        PhoneNumber = phoneNumber;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public static Clinic Create(
        ClinicId id,
        PhoneNumber phoneNumber,
        Address address,
        City city,
        ZipCode zipCode
    )
    {
        var clinic = new Clinic(id, phoneNumber, address, city, zipCode);
        clinic.RaiseDomainEvent(new ClinicCreatedDomainEvent(id.Value));
        return clinic;
    }

    public void UpdateAddress(Address newAddress)
    {
        if (!Address.Equals(newAddress))
        {
            Address = newAddress;
            RaiseDomainEvent(new ClinicAddressUpdatedDomainEvent(Id.Value, newAddress.Value));
        }
    }

    public void UpdateCity(City newCity)
    {
        if (!City.Equals(newCity))
        {
            City = newCity;
            RaiseDomainEvent(new ClinicCityUpdatedDomainEvent(Id.Value, newCity.Value));
        }
    }

    public void UpdateZipCode(ZipCode newZipCode)
    {
        if (!ZipCode.Equals(newZipCode))
        {
            ZipCode = newZipCode;
            RaiseDomainEvent(new ClinicZipCodeUpdatedDomainEvent(Id.Value, newZipCode.Value));
        }
    }

    public void UpdatePhoneNumber(PhoneNumber newPhoneNumber)
    {
        if (!PhoneNumber.Equals(newPhoneNumber))
        {
            PhoneNumber = newPhoneNumber;
            RaiseDomainEvent(new ClinicPhoneNumberUpdatedDomainEvent(Id.Value, newPhoneNumber.Value));
        }
    }

    public void Delete()
    {
        RaiseDomainEvent(new ClinicDeletedDomainEvent(Id.Value));
    }
}

using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using Shared.Contracts.Clinic.Requests;

namespace ClinicApp.IntegrationTests;

public static class ClinicTestDataFactory
{
  public static IEnumerable<object[]> GetClinicFilterTestData()
{
    yield return new object[]
    {
        CreateSampleClinics(5), 
        new ClinicFilter { City = "City1" },  
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { ZipCode = "12340" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "Address 2", PhoneNumber = "56002" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "Address 10" },
        0 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { PhoneNumber = "99999" },
        0 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "NonExistentCity" },
        0
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { ZipCode = "99999" },
        0
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City1", ZipCode = "12340" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City2", Address = "Address 2" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City3", PhoneNumber = "56003", ZipCode = "12343" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "Address 1", PhoneNumber = "56001" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { PhoneNumber = "123456" },
        0
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "Address 5" },
        0 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "123 Default St" },
        0
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "" }, 
        5 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { ZipCode = "" }, 
        5 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City0", Address = "Address 1" }, 
        0 
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City0", Address = "Address 0" }, 
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { City = "City4", ZipCode = "12344" },
        1
    };

    yield return new object[]
    {
        CreateSampleClinics(5),
        new ClinicFilter { Address = "NonExistentAddress", ZipCode = "NonExistentZip" },
        0 
    };
}

    
    public static Clinic CreateSampleClinic(
        string? city = null,
        string? phoneNumber = null,
        string? address = null,
        string? zipCode = null)
    {
        return Clinic.Create(
            ClinicId.Create(Guid.NewGuid()).Value,
            PhoneNumber.Create(phoneNumber ?? "123456").Value,
            Address.Create(address ?? "123 Default St").Value,
            City.Create(city ?? "DefaultCity").Value,
            ZipCode.Create(zipCode ?? "00000").Value
        );
    }

    public static List<Clinic> CreateSampleClinics(int count)
    {
        var clinics = new List<Clinic>();
        for (int i = 0; i < count; i++)
        {
            clinics.Add(CreateSampleClinic(
                city: $"City{i}",
                phoneNumber: $"56{i:000}",
                address: $"Address {i}",
                zipCode: $"1234{i}"
            ));
        }
        return clinics;
    }
}

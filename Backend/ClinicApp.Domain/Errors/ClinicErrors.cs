using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Domain.Errors;

public static class ClinicErrors
{
    public static Error NotFound(ClinicId id) => new(
        "Clinic.NotFound",
        $"Clinic with ID {id.Value} was not found."); 

    public static Error EmptyId => new(
        "Clinic.EmptyId",
        "Clinic ID is empty.");
    
    public static class AddressErrors
    {
        public static readonly Error Required = new(
            "Clinic.AddressRequired",
            "The clinic address is required.");

        public static readonly Error TooLong = new(
            "Clinic.AddressTooLong",
            $"The clinic address must not exceed {Address.MaxLength} characters."); 
    }

    public static class PhoneNumberErrors
    {
        public static readonly Error Required = new(
            "Clinic.PhoneNumberRequired",
            "The clinic phone number is required.");
        
        public static readonly Error TooLong = new(
            "Clinic.PhoneNumberTooLong",
            $"The clinic phone number must not exceed {PhoneNumber.MaxLength} characters.");
    }

    public static class CityErrors
    {
        public static readonly Error Required = new(
            "Clinic.CityRequired",
            "The clinic city is required.");

        public static readonly Error TooLong = new(
            "Clinic.CityTooLong",
            $"The clinic city must not exceed {City.MaxLength} characters."); 
    }

    public static class ZipCodeErrors
    {
        public static readonly Error Required = new(
            "Clinic.ZipCodeRequired",
            "The clinic zip code is required.");

        public static readonly Error TooLong = new(
            "Clinic.ZipCodeTooLong",
            $"The clinic zip code must not exceed {ZipCode.MaxLength} characters."); 
    }
}

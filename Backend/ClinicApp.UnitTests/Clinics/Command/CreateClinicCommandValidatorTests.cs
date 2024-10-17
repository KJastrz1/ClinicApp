using ClinicApp.Application.Actions.Clinics.Command.CreateClinic;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using FluentValidation.TestHelper;

namespace ClinicApp.UnitTests.Clinics.Command;

public class CreateClinicCommandValidatorTests
{
    private readonly CreateClinicCommandValidator _validator;

    public CreateClinicCommandValidatorTests()
    {
        _validator = new CreateClinicCommandValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPhoneNumberIsEmpty()
    {
        // Arrange
        var command = new CreateClinicCommand("", "123 Main St", "Some City", "12345");

        // Act
        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
            .WithErrorMessage(ClinicErrors.PhoneNumberErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenAddressIsEmpty()
    {
        var command = new CreateClinicCommand("123456789", "", "Some City", "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage(ClinicErrors.AddressErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCityIsEmpty()
    {
        var command = new CreateClinicCommand("123456789", "123 Main St", "", "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage(ClinicErrors.CityErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenZipCodeIsEmpty()
    {
        var command = new CreateClinicCommand("123456789", "123 Main St", "Some City", "");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage(ClinicErrors.ZipCodeErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPhoneNumberIsTooLong()
    {
        var command = new CreateClinicCommand(new string('A', PhoneNumber.MaxLength + 1), "123 Main St", "Some City",
            "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
            .WithErrorMessage(ClinicErrors.PhoneNumberErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenAddressIsTooLong()
    {
        var command =
            new CreateClinicCommand("123456789", new string('A', Address.MaxLength + 1), "Some City", "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage(ClinicErrors.AddressErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCityIsTooLong()
    {
        var command = new CreateClinicCommand("123456789", "123 Main St", new string('A', City.MaxLength + 1), "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage(ClinicErrors.CityErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenZipCodeIsTooLong()
    {
        var command = new CreateClinicCommand("123456789", "123 Main St", "Some City",
            new string('A', ZipCode.MaxLength + 1));

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage(ClinicErrors.ZipCodeErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        var command = new CreateClinicCommand("123456789", "123 Main St", "Some City", "12345");

        TestValidationResult<CreateClinicCommand>? result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}

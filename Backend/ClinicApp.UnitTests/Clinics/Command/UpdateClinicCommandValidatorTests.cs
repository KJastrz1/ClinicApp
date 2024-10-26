using ClinicApp.Application.UseCases.Clinics.Command.UpdateClinic;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using FluentValidation.TestHelper;
using Xunit;

namespace ClinicApp.UnitTests.Clinics.Command;

public class UpdateClinicCommandValidatorTests
{
    private readonly UpdateClinicCommandValidator _validator;

    public UpdateClinicCommandValidatorTests()
    {
        _validator = new UpdateClinicCommandValidator();
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPhoneNumberIsEmpty()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "", "123 Main St", "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
            .WithErrorMessage(ClinicErrors.PhoneNumberErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenAddressIsEmpty()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "", "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage(ClinicErrors.AddressErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCityIsEmpty()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", "", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage(ClinicErrors.CityErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenZipCodeIsEmpty()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", "Some City", "");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage(ClinicErrors.ZipCodeErrors.Required.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenPhoneNumberIsTooLong()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), new string('A', PhoneNumber.MaxLength + 1), "123 Main St", "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
            .WithErrorMessage(ClinicErrors.PhoneNumberErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenAddressIsTooLong()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", new string('A', Address.MaxLength + 1), "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address)
            .WithErrorMessage(ClinicErrors.AddressErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenCityIsTooLong()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", new string('A', City.MaxLength + 1), "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.City)
            .WithErrorMessage(ClinicErrors.CityErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenZipCodeIsTooLong()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", "Some City", new string('A', ZipCode.MaxLength + 1));

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
            .WithErrorMessage(ClinicErrors.ZipCodeErrors.TooLong.Message);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCommandIsValid()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validate_ShouldNotHaveError_WhenPhoneNumberIsNull()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), null, "123 Main St", "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenAddressIsNull()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", null, "Some City", "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenCityIsNull()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", null, "12345");

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenZipCodeIsNull()
    {
        // Arrange
        var command = new UpdateClinicCommand(Guid.NewGuid(), "123456789", "123 Main St", "Some City", null);

        // Act
        TestValidationResult<UpdateClinicCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}

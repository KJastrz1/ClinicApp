using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Clinics.Command.CreateClinic;

internal class CreateClinicCommandValidator : AbstractValidator<CreateClinicCommand>
{
    public CreateClinicCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(ClinicErrors.PhoneNumberErrors.Required.Message)
            .MaximumLength(PhoneNumber.MaxLength)
            .WithMessage(ClinicErrors.PhoneNumberErrors.TooLong.Message);
        ;

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(ClinicErrors.AddressErrors.Required.Message)
            .MaximumLength(Address.MaxLength)
            .WithMessage(ClinicErrors.AddressErrors.TooLong.Message);

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage(ClinicErrors.CityErrors.Required.Message)
            .MaximumLength(City.MaxLength)
            .WithMessage(ClinicErrors.CityErrors.TooLong.Message);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage(ClinicErrors.ZipCodeErrors.Required.Message)
            .MaximumLength(ZipCode.MaxLength)
            .WithMessage(ClinicErrors.ZipCodeErrors.TooLong.Message);
    }
}

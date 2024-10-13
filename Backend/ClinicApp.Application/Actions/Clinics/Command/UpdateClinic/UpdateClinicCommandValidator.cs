using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.Actions.Clinics.Command.UpdateClinic;

internal class UpdateClinicCommandValidator : AbstractValidator<UpdateClinicCommand>
{
    public UpdateClinicCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .When(x => x.PhoneNumber != null)
            .WithMessage(ClinicErrors.PhoneNumberErrors.Required.Message)
            .Must(value => value == null || PhoneNumber.Create(value).IsSuccess)
            .WithMessage(ClinicErrors.PhoneNumberErrors.Invalid.Message);

        RuleFor(x => x.Address)
            .NotEmpty()
            .When(x => x.Address != null)
            .WithMessage(ClinicErrors.AddressErrors.Required.Message)
            .MaximumLength(Address.MaxLength)
            .When(x => x.Address != null)
            .WithMessage(ClinicErrors.AddressErrors.TooLong.Message);

        RuleFor(x => x.City)
            .NotEmpty()
            .When(x => x.City != null)
            .WithMessage(ClinicErrors.CityErrors.Required.Message)
            .MaximumLength(City.MaxLength)
            .When(x => x.City != null)
            .WithMessage(ClinicErrors.CityErrors.TooLong.Message);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .When(x => x.ZipCode != null)
            .WithMessage(ClinicErrors.ZipCodeErrors.Required.Message)
            .MaximumLength(ZipCode.MaxLength)
            .WithMessage(ClinicErrors.ZipCodeErrors.TooLong.Message);
    }
}

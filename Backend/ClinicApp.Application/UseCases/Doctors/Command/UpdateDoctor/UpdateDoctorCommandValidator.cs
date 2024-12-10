using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Doctors.Command.UpdateDoctor;

internal class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
{
    public UpdateDoctorCommandValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty()
            .WithMessage(DoctorErrors.EmptyId.Message);

        RuleFor(x => x.FirstName)
            .MaximumLength(FirstName.MaxLength)
            .WithMessage(UserProfileErrors.FirstNameErrors.TooLong.Message)
            .When(x => x.FirstName != null);

        RuleFor(x => x.LastName)
            .MaximumLength(LastName.MaxLength)
            .WithMessage(UserProfileErrors.LastNameErrors.TooLong.Message)
            .When(x => x.LastName != null);

        RuleFor(x => x.MedicalLicenseNumber)
            .MaximumLength(MedicalLicenseNumber.MaxLength)
            .WithMessage(DoctorErrors.MedicalLicenseNumberErrors.TooLong.Message)
            .When(x => x.MedicalLicenseNumber != null);

        RuleFor(x => x.Specialties)
            .Must(specialties => specialties == null || specialties.Count > 0)
            .WithMessage(DoctorErrors.SpecialtyErrors.Empty.Message);

        RuleFor(x => x.Bio)
            .MaximumLength(Bio.MaxLength)
            .WithMessage(DoctorErrors.BioErrors.TooLong.Message)
            .When(x => x.Bio != null);

        RuleFor(x => x.AcademicTitle)
            .MaximumLength(AcademicTitle.MaxLength)
            .WithMessage(DoctorErrors.AcademicTitleErrors.TooLong.Message)
            .When(x => x.AcademicTitle != null);
    }
}

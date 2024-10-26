using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Users.ValueObjects;
using FluentValidation;

namespace ClinicApp.Application.UseCases.Doctors.Command.CreateDoctor;

internal class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(UserBaseErrors.FirstNameErrors.Empty.Message)
            .MaximumLength(FirstName.MaxLength)
            .WithMessage(UserBaseErrors.FirstNameErrors.TooLong.Message);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(UserBaseErrors.LastNameErrors.Empty.Message)
            .MaximumLength(LastName.MaxLength)
            .WithMessage(UserBaseErrors.LastNameErrors.TooLong.Message);

        RuleFor(x => x.MedicalLicenseNumber)
            .NotEmpty()
            .WithMessage(DoctorErrors.MedicalLicenseNumberErrors.Empty.Message)
            .MaximumLength(MedicalLicenseNumber.MaxLength)
            .WithMessage(DoctorErrors.MedicalLicenseNumberErrors.TooLong.Message);

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

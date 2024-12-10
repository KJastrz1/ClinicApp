using ClinicApp.Application.Abstractions.Messaging;
using ClinicApp.Application.RepositoryInterfaces.Write;
using ClinicApp.Domain.Errors;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Domain.Shared;

namespace ClinicApp.Application.UseCases.Doctors.Command.UpdateDoctor;

internal sealed class UpdateDoctorCommandHandler : ICommandHandler<UpdateDoctorCommand, Guid>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClinicRepository _clinicRepository;

    public UpdateDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
      
        IClinicRepository clinicRepository)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
        _clinicRepository = clinicRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
    {
        DoctorId doctorId = DoctorId.Create(request.DoctorId).Value;
        Doctor? doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);

        if (doctor is null)
        {
            return Result.Failure<Guid>(DoctorErrors.NotFound(doctorId));
        }

        FirstName firstName = null;
        LastName lastName = null;
        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            firstName = FirstName.Create(request.FirstName).Value;
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            lastName = LastName.Create(request.LastName).Value;
        }

        if (firstName is not null && lastName is not null)
        {
            doctor.ChangeName(firstName, lastName);
        }
        else if (firstName is not null)
        {
            doctor.ChangeName(firstName, doctor.LastName);
        }
        else if (lastName is not null)
        {
            doctor.ChangeName(doctor.FirstName, lastName);
        }

        if (!string.IsNullOrWhiteSpace(request.MedicalLicenseNumber))
        {
            MedicalLicenseNumber licenseNumber = MedicalLicenseNumber.Create(request.MedicalLicenseNumber).Value;
            doctor.ChangeMedicalLicenseNumber(licenseNumber);
        }

        if (request.Specialties != null)
        {
            doctor.UpdateSpecialties(request.Specialties.Select(s => Specialty.Create(s).Value).ToList());
        }

        if (!string.IsNullOrWhiteSpace(request.Bio))
        {
            Bio bio = Bio.Create(request.Bio).Value;
            doctor.ChangeBio(bio);
        }

        if (!string.IsNullOrWhiteSpace(request.AcademicTitle))
        {
            AcademicTitle academicTitle = AcademicTitle.Create(request.AcademicTitle).Value;
            doctor.ChangeAcademicTitle(academicTitle);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return doctor.Id.Value;
    }
}

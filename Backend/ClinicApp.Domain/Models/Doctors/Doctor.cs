using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Clinics;
using ClinicApp.Domain.Models.Doctors.DomainEvents;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.Users;
using ClinicApp.Domain.Models.Users.ValueObjects;
using ClinicApp.Domain.Shared;
using ClinicApp.Domain.Models.Accounts;
using ClinicApp.Domain.Models.Clinics.ValueObjects;
using ClinicApp.Domain.Models.Employees;

namespace ClinicApp.Domain.Models.Doctors;

public class Doctor : Employee
{
    public MedicalLicenseNumber MedicalLicenseNumber { get; private set; }
    public Bio? Bio { get; private set; }
    public AcademicTitle? AcademicTitle { get; private set; }

    private List<Specialty> _specialties = new List<Specialty>();
    public IReadOnlyList<Specialty> Specialties => _specialties.AsReadOnly();

    private readonly List<DoctorSchedule> _schedules = new List<DoctorSchedule>();
    public IReadOnlyList<DoctorSchedule> Schedules => _schedules.AsReadOnly();

    private Doctor() { }

    private Doctor(
        UserId id,
        FirstName firstName,
        LastName lastName,
        MedicalLicenseNumber medicalLicenseNumber,
        List<Specialty>? specialties = null,
        Bio? bio = null,
        AcademicTitle? academicTitle = null,
        Account? account = null)
        : base(id, firstName, lastName, UserType.Doctor, account)
    {
        MedicalLicenseNumber = medicalLicenseNumber;
        _specialties = specialties ?? new List<Specialty>();
        Bio = bio;
        AcademicTitle = academicTitle;
    }

    public static Doctor Create(
        UserId id,
        FirstName firstName,
        LastName lastName,
        MedicalLicenseNumber medicalLicenseNumber,
        List<Specialty>? specialties = null,
        Bio? bio = null,
        AcademicTitle? academicTitle = null,
        Account? account = null)
    {
        var doctor = new Doctor(id, firstName, lastName, medicalLicenseNumber, specialties, bio, academicTitle,
            account);
        doctor.RaiseDomainEvent(new DoctorRegisteredDomainEvent(id.Value));
        return doctor;
    }

    public Result<Doctor> ChangeMedicalLicenseNumber(MedicalLicenseNumber newLicenseNumber)
    {
        if (MedicalLicenseNumber.Equals(newLicenseNumber))
        {
            return Result.Success(this);
        }

        MedicalLicenseNumber = newLicenseNumber;
        RaiseDomainEvent(new DoctorLicenseNumberChangedDomainEvent(Id.Value, newLicenseNumber.Value));
        return Result.Success(this);
    }

    public Result<Doctor> UpdateSpecialties(List<Specialty> newSpecialties)
    {
        foreach (Specialty specialty in Specialties.ToList())
        {
            RemoveSpecialty(specialty);
        }

        foreach (Specialty specialty in newSpecialties)
        {
            AddSpecialty(specialty);
        }

        return Result.Success(this);
    }

    public Result<Doctor> AddSpecialty(Specialty specialty)
    {
        if (_specialties.Contains(specialty))
        {
            return Result.Success(this);
        }

        _specialties.Add(specialty);
        RaiseDomainEvent(new DoctorSpecialtyAddedDomainEvent(Id.Value, specialty.Value));
        return Result.Success(this);
    }

    public Result<Doctor> RemoveSpecialty(Specialty specialty)
    {
        if (!_specialties.Remove(specialty))
        {
            return Result.Success(this);
        }

        RaiseDomainEvent(new DoctorSpecialtyRemovedDomainEvent(Id.Value, specialty.Value));
        return Result.Success(this);
    }

    public Result<Doctor> AddSchedule(DoctorSchedule schedule)
    {
        if (_schedules.Contains(schedule))
        {
            return Result.Success(this);
        }

        _schedules.Add(schedule);
        RaiseDomainEvent(new DoctorScheduleAddedDomainEvent(Id.Value, schedule.Day.Value, schedule.StartTime.Value,
            schedule.EndTime.Value));
        return Result.Success(this);
    }

    public Result<Doctor> RemoveSchedule(DoctorScheduleId scheduleId)
    {
        DoctorSchedule? scheduleToRemove = _schedules.FirstOrDefault(schedule => schedule.Id.Equals(scheduleId));

        if (scheduleToRemove == null)
        {
            return Result.Success(this);
        }

        _schedules.Remove(scheduleToRemove);

        RaiseDomainEvent(new DoctorScheduleRemovedDomainEvent(Id.Value, scheduleToRemove.Day.Value,
            scheduleToRemove.StartTime.Value, scheduleToRemove.EndTime.Value));

        return Result.Success(this);
    }


    public Result<Doctor> ChangeBio(Bio newBio)
    {
        Bio = newBio;
        RaiseDomainEvent(new DoctorBioChangedDomainEvent(Id.Value, newBio.Value));
        return Result.Success(this);
    }

    public Result<Doctor> ChangeAcademicTitle(AcademicTitle newAcademicTitle)
    {
        AcademicTitle = newAcademicTitle;
        RaiseDomainEvent(new DoctorAcademicTitleChangedDomainEvent(Id.Value, newAcademicTitle.Value));
        return Result.Success(this);
    }

    public Result<bool> Delete()
    {
        RaiseDomainEvent(new DoctorDeletedDomainEvent(Id.Value));
        return Result.Success(true);
    }
}

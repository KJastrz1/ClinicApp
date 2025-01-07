using Bogus; 
using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Doctors;
using ClinicApp.Domain.Models.Doctors.ValueObjects;
using ClinicApp.Domain.Models.UserProfiles.ValueObjects;
using ClinicApp.Infrastructure.Authentication.IdentityCore;
using ClinicApp.Infrastructure.Database.Contexts;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Infrastructure.Database.DataSeeders;

public class DoctorSeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly WriteDbContext _dbContext;
    private readonly Faker _faker;

    public DoctorSeeder(UserManager<User> userManager, WriteDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _faker = new Faker(); 
    }

    public async Task SeedAsync()
    {
        
        IList<User> doctors = await _userManager.GetUsersInRoleAsync(UserRoleEnum.Doctor.ToString());

        foreach (User user in doctors)
        {
          
            if (_dbContext.Doctors.Any(d => d.Id == UserId.Create(user.Id).Value))
            {
                continue;
            }
      
            string? firstName = _faker.Name.FirstName();
            string? lastName = _faker.Name.LastName();
            string medicalLicenseNumber = "MLN-" + _faker.Random.AlphaNumeric(8)!.ToUpper();
            List<Specialty> specialties = GenerateSpecialties();
            string? bio = _faker.Lorem.Sentence(10, 20);
            string? academicTitle = _faker.PickRandom("MD", "PhD", "DO", "DDS");
           
            var doctor = Doctor.Create(
                id: UserId.Create(user.Id).Value,
                firstName: FirstName.Create(firstName).Value,
                lastName: LastName.Create(lastName).Value,
                medicalLicenseNumber: MedicalLicenseNumber.Create(medicalLicenseNumber).Value,
                specialties: specialties,
                bio: Bio.Create(bio).Value,
                academicTitle: AcademicTitle.Create(academicTitle).Value
            );

            _dbContext.Doctors.Add(doctor);
        }

        await _dbContext.SaveChangesAsync();
    }

    private List<Specialty> GenerateSpecialties()
    {
        string[] specialties = new[] { "Cardiology", "Dermatology", "Neurology", "Pediatrics", "Psychiatry", "Radiology", "Surgery" };
        var selectedSpecialties = _faker.PickRandom(specialties, _faker.Random.Int(1, 3)).ToList();
        return selectedSpecialties.Select(specialty => Specialty.Create(specialty).Value).ToList();
    }
}

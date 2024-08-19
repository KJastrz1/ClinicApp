using ClinicApp.Domain.Enums;
using ClinicApp.Domain.Models.Patients;
using ClinicApp.Domain.Models.User.ValueObjects;
using ClinicApp.Domain.Models.UserBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations;


public class UserEntityConfiguration<TUser,TId> :IEntityTypeConfiguration<UserBase<TId>>
    where TUser : UserBase<TId>
    where TId : UserId
{
    public void Configure(EntityTypeBuilder<UserBase<TId>> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                value => (TId)Activator.CreateInstance(typeof(TId), value));
        
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value, // Z Email do string
                value => Email.Create(value).Value) // Z string do Email
            .IsRequired();

        // Konfiguracja dla FirstName
        builder.Property(u => u.FirstName)
            .HasConversion(
                firstName => firstName.Value, // Z FirstName do string
                value => FirstName.Create(value).Value) // Z string do FirstName
            .IsRequired();

        // Konfiguracja dla LastName
        builder.Property(u => u.LastName)
            .HasConversion(
                lastName => lastName.Value, // Z LastName do string
                value => LastName.Create(value).Value) // Z string do LastName
            .IsRequired();

        // Konfiguracja dla IsActivated
        builder.Property(u => u.IsActivated)
            .IsRequired();

        // Konfiguracja dla CreatedOnUtc
        builder.Property(u => u.CreatedOnUtc)
            .IsRequired();

        // Konfiguracja dla ModifiedOnUtc
        builder.Property(u => u.ModifiedOnUtc);
    }
}

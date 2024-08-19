using ClinicApp.Domain.Models.User;
using ClinicApp.Domain.Models.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicApp.Infrastructure.Database.Configurations;

public class UserEntityConfiguration<TUser, TId> : IEntityTypeConfiguration<UserBase<TId>>
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
                email => email.Value,
                value => Email.Create(value).Value)
            .IsRequired();


        builder.Property(u => u.FirstName)
            .HasConversion(
                firstName => firstName.Value,
                value => FirstName.Create(value).Value)
            .IsRequired();


        builder.Property(u => u.LastName)
            .HasConversion(
                lastName => lastName.Value,
                value => LastName.Create(value).Value)
            .IsRequired();


        builder.Property(u => u.IsActivated)
            .IsRequired();


        builder.Property(u => u.CreatedOnUtc)
            .IsRequired();


        builder.Property(u => u.ModifiedOnUtc);
    }
}

using System;

namespace ClinicApp.Domain.Enums;

public sealed class BasicRoles
{
    public static readonly BasicRoles Admin = new(Guid.Parse("A1B2C3D4-E5F6-4747-B8C9-D0E1F2A3B4C5"), "Admin");
    public static readonly BasicRoles Doctor = new(Guid.Parse("B2C3D4E5-F6A1-4848-C9D0-E1F2A3B4C5D6"), "Doctor");
    public static readonly BasicRoles Patient = new(Guid.Parse("C3D4E5F6-A1B2-4949-D0E1-F2A3B4C5D6E7"), "Patient");
    public static readonly BasicRoles SuperAdmin = new(Guid.Parse("D4E5F6A1-B2C3-4A4A-E1F2-A3B4C5D6E7F8"), "SuperAdmin");

    public Guid Id { get; }
    public string Name { get; }

    private BasicRoles(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static BasicRoles[] All => new[] { Admin, Doctor, Patient, SuperAdmin };

    public static BasicRoles? FromId(Guid id)
    {
        return Array.Find(All, role => role.Id == id);
    }

    public static BasicRoles? FromName(string name)
    {
        return Array.Find(All, role => role.Name == name);
    }

    public override string ToString() => Name;
}

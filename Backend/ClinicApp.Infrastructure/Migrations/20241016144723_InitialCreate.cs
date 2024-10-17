using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clinics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageConsumers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessageConsumers", x => new { x.Id, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "text", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoles_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalLicenseNumber = table.Column<string>(type: "text", nullable: false),
                    SpecialtiesString = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    AcademicTitle = table.Column<string>(type: "text", nullable: true),
                    ClinicId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Clinics_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Doctors_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_User_Id",
                        column: x => x.Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    VisitDuration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSchedule_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    Value = table.Column<string>(type: "text", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => new { x.DoctorId, x.Value });
                    table.ForeignKey(
                        name: "FK_Specialty_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AssignRole" },
                    { 2, "RevokeRole" },
                    { 3, "CreateRole" },
                    { 4, "DeleteRole" },
                    { 5, "AddPermissionToRole" },
                    { 6, "RemovePermissionFromRole" },
                    { 7, "ReadPatient" },
                    { 8, "CreatePatient" },
                    { 9, "UpdatePatient" },
                    { 10, "DeletePatient" },
                    { 11, "ReadDoctor" },
                    { 12, "CreateDoctor" },
                    { 13, "UpdateDoctor" },
                    { 14, "DeleteDoctor" },
                    { 15, "ReadClinic" },
                    { 16, "CreateClinic" },
                    { 17, "UpdateClinic" },
                    { 18, "DeleteClinic" },
                    { 19, "ReadAppointment" },
                    { 20, "CreateAppointment" },
                    { 21, "UpdateAppointment" },
                    { 22, "DeleteAppointment" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5"), "Admin" },
                    { new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6"), "Doctor" },
                    { new Guid("c3d4e5f6-a1b2-4949-d0e1-f2a3b4c5d6e7"), "Patient" },
                    { new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8"), "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 7, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 8, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 9, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 10, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 11, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 12, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 13, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 14, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 15, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 16, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 17, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 18, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 19, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 20, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 21, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 22, new Guid("a1b2c3d4-e5f6-4747-b8c9-d0e1f2a3b4c5") },
                    { 7, new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6") },
                    { 8, new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6") },
                    { 9, new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6") },
                    { 10, new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6") },
                    { 11, new Guid("b2c3d4e5-f6a1-4848-c9d0-e1f2a3b4c5d6") },
                    { 11, new Guid("c3d4e5f6-a1b2-4949-d0e1-f2a3b4c5d6e7") },
                    { 1, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 2, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 3, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 4, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 5, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 6, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 7, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 8, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 9, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 10, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 11, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 12, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 13, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 14, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 15, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 16, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 17, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 18, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 19, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 20, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 21, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") },
                    { 22, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ClinicId",
                table: "Doctors",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_DoctorId",
                table: "DoctorSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountId",
                table: "User",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumers");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Specialty");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}

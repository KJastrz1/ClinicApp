using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "AssignRole" },
                    { 2, "RevokeRole" },
                    { 3, "CreateRole" },
                    { 4, "DeleteRole" },
                    { 5, "AddPermissionsToRole" },
                    { 6, "RemovePermissionFromRole" },
                    { 7, "ReadPatient" },
                    { 8, "CreatePatient" },
                    { 9, "UpdatePatient" },
                    { 10, "DeletePatient" },
                    { 11, "ReadDoctor" },
                    { 12, "CreateDoctor" },
                    { 13, "UpdateDoctor" },
                    { 14, "DeleteDoctor" }
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
                    { 14, new Guid("d4e5f6a1-b2c3-4a4a-e1f2-a3b4c5d6e7f8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

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
                name: "OutboxMessageConsumers");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}

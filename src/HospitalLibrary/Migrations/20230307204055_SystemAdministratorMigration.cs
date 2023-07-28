using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SystemAdministratorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Discriminator", "Email", "Gender", "Name", "Password", "Surname", "Ucid", "UserRole", "Address_City", "Address_Country", "Address_Number", "Address_Street" },
                values: new object[] { 4, new DateTime(1992, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "SystemAdministrator", "milos@gmail.com", 0, "Milos", "asd", "Petrovic", 111, 2, "Novi Sad", "Srbija", 122, "Temerinska" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

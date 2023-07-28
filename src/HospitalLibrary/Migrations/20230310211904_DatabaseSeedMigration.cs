using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "Email", "Address_Street" },
                values: new object[] { new DateTime(1990, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "marko@gmail.com", "Dunavska" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "Email", "Name", "Surname", "Address_Number", "Address_Street" },
                values: new object[] { new DateTime(1983, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "jelena@gmail.com", "Jelena", "Tomic", 5, "Strazilovska" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BirthDate", "Email" },
                values: new object[] { new DateTime(1978, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "jovan@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "Email", "Address_Street" },
                values: new object[] { new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "vaskenscz@gmail.com", "Njegoseva" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "Email", "Name", "Surname", "Address_Number", "Address_Street" },
                values: new object[] { new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "asdf@gmail.com", "Petar", "Markovic", 2, "Njegoseva" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BirthDate", "Email" },
                values: new object[] { new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "asdfg" });
        }
    }
}

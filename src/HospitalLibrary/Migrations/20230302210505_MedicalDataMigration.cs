using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class MedicalDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalData",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BloodPressure = table.Column<double>(type: "double precision", nullable: false),
                    BloodSugar = table.Column<double>(type: "double precision", nullable: false),
                    BodyFat = table.Column<double>(type: "double precision", nullable: false),
                    BodyWeight = table.Column<double>(type: "double precision", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalData", x => new { x.PatientId, x.Id });
                    table.ForeignKey(
                        name: "FK_MedicalData_Users_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalData");
        }
    }
}

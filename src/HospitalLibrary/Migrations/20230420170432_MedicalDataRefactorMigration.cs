using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class MedicalDataRefactorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalData",
                table: "MedicalData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalData",
                table: "MedicalData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalData_PatientId",
                table: "MedicalData",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalData",
                table: "MedicalData");

            migrationBuilder.DropIndex(
                name: "IX_MedicalData_PatientId",
                table: "MedicalData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalData",
                table: "MedicalData",
                columns: new[] { "PatientId", "Id" });
        }
    }
}

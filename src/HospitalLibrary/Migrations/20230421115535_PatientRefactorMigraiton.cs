using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class PatientRefactorMigraiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ChosenDoctorId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ChosenDoctorId",
                table: "Users",
                column: "ChosenDoctorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ChosenDoctorId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ChosenDoctorId",
                table: "Users",
                column: "ChosenDoctorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

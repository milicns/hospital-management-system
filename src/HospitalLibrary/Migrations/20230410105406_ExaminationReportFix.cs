using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalLibrary.Migrations
{
    /// <inheritdoc />
    public partial class ExaminationReportFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorReferralId",
                table: "Examinations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_DoctorReferralId",
                table: "Examinations",
                column: "DoctorReferralId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_DoctorReferrals_DoctorReferralId",
                table: "Examinations",
                column: "DoctorReferralId",
                principalTable: "DoctorReferrals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_DoctorReferrals_DoctorReferralId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_DoctorReferralId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "DoctorReferralId",
                table: "Examinations");
        }
    }
}

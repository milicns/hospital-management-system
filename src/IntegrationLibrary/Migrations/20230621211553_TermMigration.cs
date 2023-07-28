using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IntegrationLibrary.Migrations
{
    /// <inheritdoc />
    public partial class TermMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.CreateTable(
                name: "AvailableDonationTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableDonationTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPatients",
                columns: table => new
                {
                    TermId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientEmail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPatients", x => new { x.TermId, x.Id });
                    table.ForeignKey(
                        name: "FK_ScheduledPatients_AvailableDonationTerms_TermId",
                        column: x => x.TermId,
                        principalTable: "AvailableDonationTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledPatients");

            migrationBuilder.DropTable(
                name: "AvailableDonationTerms");

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BloodBankNewsId = table.Column<int>(type: "integer", nullable: true),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Term_BloodBankNews_BloodBankNewsId",
                        column: x => x.BloodBankNewsId,
                        principalTable: "BloodBankNews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Term_BloodBankNewsId",
                table: "Term",
                column: "BloodBankNewsId");
        }
    }
}

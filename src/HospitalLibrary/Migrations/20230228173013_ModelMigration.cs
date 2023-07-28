using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    public partial class ModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Ucid = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Address_Country = table.Column<string>(type: "text", nullable: true),
                    Address_City = table.Column<string>(type: "text", nullable: true),
                    Address_Street = table.Column<string>(type: "text", nullable: true),
                    Address_Number = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserRole = table.Column<int>(type: "integer", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Specialization = table.Column<int>(type: "integer", nullable: true),
                    BloodType = table.Column<int>(type: "integer", nullable: true),
                    ChosenDoctorId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_ChosenDoctorId",
                        column: x => x.ChosenDoctorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Discriminator", "Email", "Gender", "Name", "Password", "Specialization", "Surname", "Ucid", "UserRole", "Address_City", "Address_Country", "Address_Number", "Address_Street" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "vaskenscz@gmail.com", 0, "Marko", "asd", 3, "Markovic", 334, 1, "Novi Sad", "Srbija", 12, "Njegoseva" },
                    { 2, new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "vaskenscz@gmail.com", 0, "Petar", "asd", 3, "Markovic", 2234, 1, "Novi Sad", "Srbija", 2, "Njegoseva" },
                    { 3, new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doctor", "asdfg", 0, "Jovan", "asd", 1, "Markovic", 5234, 1, "Novi Sad", "Srbija", 22, "Njegoseva" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChosenDoctorId",
                table: "Users",
                column: "ChosenDoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

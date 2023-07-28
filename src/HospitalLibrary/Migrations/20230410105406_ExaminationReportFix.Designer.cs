﻿// <auto-generated />
using System;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HospitalLibrary.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    [Migration("20230410105406_ExaminationReportFix")]
    partial class ExaminationReportFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HospitalLibrary.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HospitalLibrary.DoctorReferral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("Specialization")
                        .HasColumnType("integer");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("DoctorReferrals");
                });

            modelBuilder.Entity("HospitalLibrary.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int?>("DoctorReferralId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ExaminationTerm")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("DoctorReferralId");

                    b.HasIndex("PatientId");

                    b.ToTable("Examinations");
                });

            modelBuilder.Entity("HospitalLibrary.ExaminationReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("integer");

                    b.Property<string>("Treatment")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExaminationId")
                        .IsUnique();

                    b.ToTable("ExaminationReports");
                });

            modelBuilder.Entity("HospitalLibrary.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<int>("Ucid")
                        .HasColumnType("integer");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("HospitalLibrary.Doctor", b =>
                {
                    b.HasBaseType("HospitalLibrary.User");

                    b.Property<int>("Specialization")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue("Doctor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(1990, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "marko@gmail.com",
                            Gender = 0,
                            Name = "Marko",
                            Password = "asd",
                            Surname = "Markovic",
                            Ucid = 334,
                            UserRole = 1,
                            Specialization = 3
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(1983, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jelena@gmail.com",
                            Gender = 0,
                            Name = "Jelena",
                            Password = "asd",
                            Surname = "Tomic",
                            Ucid = 2234,
                            UserRole = 1,
                            Specialization = 3
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(1978, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jovan@gmail.com",
                            Gender = 0,
                            Name = "Jovan",
                            Password = "asd",
                            Surname = "Markovic",
                            Ucid = 5234,
                            UserRole = 1,
                            Specialization = 1
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Patient", b =>
                {
                    b.HasBaseType("HospitalLibrary.User");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<int?>("ChosenDoctorId")
                        .HasColumnType("integer");

                    b.HasIndex("ChosenDoctorId");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("HospitalLibrary.SystemAdministrator", b =>
                {
                    b.HasBaseType("HospitalLibrary.User");

                    b.HasDiscriminator().HasValue("SystemAdministrator");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            BirthDate = new DateTime(1992, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "milos@gmail.com",
                            Gender = 0,
                            Name = "Milos",
                            Password = "asd",
                            Surname = "Petrovic",
                            Ucid = 111,
                            UserRole = 2
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Blog", b =>
                {
                    b.HasOne("HospitalLibrary.Doctor", "Author")
                        .WithMany("WrittenBlogs")
                        .HasForeignKey("AuthorId");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("HospitalLibrary.DoctorReferral", b =>
                {
                    b.HasOne("HospitalLibrary.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalLibrary.Examination", b =>
                {
                    b.HasOne("HospitalLibrary.Doctor", "Doctor")
                        .WithMany("Examinations")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospitalLibrary.DoctorReferral", "DoctorReferral")
                        .WithMany()
                        .HasForeignKey("DoctorReferralId");

                    b.HasOne("HospitalLibrary.Patient", "Patient")
                        .WithMany("Examinations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("DoctorReferral");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HospitalLibrary.ExaminationReport", b =>
                {
                    b.HasOne("HospitalLibrary.Examination", "Examination")
                        .WithOne("ExaminationReport")
                        .HasForeignKey("HospitalLibrary.ExaminationReport", "ExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("HospitalLibrary.User", b =>
                {
                    b.OwnsOne("HospitalLibrary.Address", "Address", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<int>("Number")
                                .HasColumnType("integer");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = 1,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 12,
                                    Street = "Dunavska"
                                },
                                new
                                {
                                    UserId = 2,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 5,
                                    Street = "Strazilovska"
                                },
                                new
                                {
                                    UserId = 3,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 22,
                                    Street = "Njegoseva"
                                },
                                new
                                {
                                    UserId = 4,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 122,
                                    Street = "Temerinska"
                                });
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("HospitalLibrary.Patient", b =>
                {
                    b.HasOne("HospitalLibrary.Doctor", "ChosenDoctor")
                        .WithMany("Patients")
                        .HasForeignKey("ChosenDoctorId");

                    b.OwnsMany("HospitalLibrary.MedicalData", "MedicalData", b1 =>
                        {
                            b1.Property<int>("PatientId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<double>("BloodPressure")
                                .HasColumnType("double precision");

                            b1.Property<double>("BloodSugar")
                                .HasColumnType("double precision");

                            b1.Property<double>("BodyFat")
                                .HasColumnType("double precision");

                            b1.Property<double>("BodyWeight")
                                .HasColumnType("double precision");

                            b1.Property<DateTime>("MeasurementDate")
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("PatientId", "Id");

                            b1.ToTable("MedicalData");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.OwnsMany("HospitalLibrary.Period", "MenstrualPeriods", b1 =>
                        {
                            b1.Property<int>("PatientId")
                                .HasColumnType("integer");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<DateTime>("End")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("PatientId", "Id");

                            b1.ToTable("Period");

                            b1.WithOwner()
                                .HasForeignKey("PatientId");
                        });

                    b.Navigation("ChosenDoctor");

                    b.Navigation("MedicalData");

                    b.Navigation("MenstrualPeriods");
                });

            modelBuilder.Entity("HospitalLibrary.Examination", b =>
                {
                    b.Navigation("ExaminationReport");
                });

            modelBuilder.Entity("HospitalLibrary.Doctor", b =>
                {
                    b.Navigation("Examinations");

                    b.Navigation("Patients");

                    b.Navigation("WrittenBlogs");
                });

            modelBuilder.Entity("HospitalLibrary.Patient", b =>
                {
                    b.Navigation("Examinations");
                });
#pragma warning restore 612, 618
        }
    }
}

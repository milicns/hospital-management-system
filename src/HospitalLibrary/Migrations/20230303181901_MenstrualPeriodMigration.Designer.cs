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
    [Migration("20230303181901_MenstrualPeriodMigration")]
    partial class MenstrualPeriodMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                            BirthDate = new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vaskenscz@gmail.com",
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
                            BirthDate = new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vaskenscz@gmail.com",
                            Gender = 0,
                            Name = "Petar",
                            Password = "asd",
                            Surname = "Markovic",
                            Ucid = 2234,
                            UserRole = 1,
                            Specialization = 3
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(2000, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "asdfg",
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
                                    Street = "Njegoseva"
                                },
                                new
                                {
                                    UserId = 2,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 2,
                                    Street = "Njegoseva"
                                },
                                new
                                {
                                    UserId = 3,
                                    City = "Novi Sad",
                                    Country = "Srbija",
                                    Number = 22,
                                    Street = "Njegoseva"
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

            modelBuilder.Entity("HospitalLibrary.Doctor", b =>
                {
                    b.Navigation("Patients");
                });
#pragma warning restore 612, 618
        }
    }
}

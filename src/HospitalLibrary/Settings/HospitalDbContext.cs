
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.User.Model;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<User.Model.User> Users { get; set; }
        public DbSet<Patient.Model.Patient> Patients { get; set; }
        public DbSet<Doctor.Model.Doctor> Doctors { get; set; }
        public DbSet<SystemAdministrator.Model.SystemAdministrator> SystemAdministrators { get; set; }
        public DbSet<Blog.Model.Blog> Blogs { get; set; }
        public DbSet<Examination.Model.Examination> Examinations { get; set; }
        public DbSet<ExaminationReport.Model.ExaminationReport> ExaminationReports { get; set; }
        public DbSet<DoctorReferral.Model.DoctorReferral> DoctorReferrals { get; set; }
        public DbSet<News.Model.News> News { get; set; }
        public DbSet<MedicalData.Model.MedicalData> MedicalData { get; set; }
        public DbSet<MenstrualPeriod.Model.MenstrualPeriod> MenstrualPeriods { get; set; }


        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor.Model.Doctor>(b =>
            {
                b.HasData(new Doctor.Model.Doctor
                {
                    Id = 1, Name = "Marko", Surname = "Markovic", Ucid = 334, Email = "marko@gmail.com",
                    BirthDate = new DateTime(1990, 8, 5), Password = "asd", Specialization = Specialization.Generalist,
                    Patients = new List<Patient.Model.Patient>(),
                    UserRole = UserRole.Doctor, Gender = Gender.Male
                });
                b.OwnsOne(a => a.Address).HasData(new
                    { UserId = 1, Country = "Srbija", City = "Novi Sad", Street = "Dunavska", Number = 12 });
                b.HasData(new Doctor.Model.Doctor
                {
                    Id = 2, Name = "Jelena", Surname = "Tomic", Ucid = 2234, Email = "jelena@gmail.com",
                    BirthDate = new DateTime(1983, 12, 15), Password = "asd",
                    Specialization = Specialization.Generalist,
                    Patients = new List<Patient.Model.Patient>(),
                    UserRole = UserRole.Doctor, Gender = Gender.Male
                });
                b.OwnsOne(a => a.Address).HasData(new
                    { UserId = 2, Country = "Srbija", City = "Novi Sad", Street = "Strazilovska", Number = 5 });
                b.HasData(new Doctor.Model.Doctor
                {
                    Id = 3, Name = "Jovan", Surname = "Markovic", Ucid = 5234, Email = "jovan@gmail.com",
                    BirthDate = new DateTime(1978, 4, 11), Password = "asd",
                    Specialization = Specialization.Nutritionist,
                    Patients = new List<Patient.Model.Patient>(),
                    UserRole = UserRole.Doctor, Gender = Gender.Male
                });
                b.OwnsOne(a => a.Address).HasData(new
                    { UserId = 3, Country = "Srbija", City = "Novi Sad", Street = "Njegoseva", Number = 22 });

            });

            modelBuilder.Entity<SystemAdministrator.Model.SystemAdministrator>(b =>
            {
                b.HasData(new SystemAdministrator.Model.SystemAdministrator
                {
                    Id = 4, Name = "Milos", Surname = "Petrovic", Ucid = 111, Email = "milos@gmail.com",
                    Password = "asd", BirthDate = new DateTime(1992, 12, 2),
                    UserRole = UserRole.SystemAdministrator, Gender = Gender.Male
                });
                b.OwnsOne(a => a.Address).HasData(new
                    { UserId = 4, Country = "Srbija", City = "Novi Sad", Street = "Temerinska", Number = 122 });
            });

            modelBuilder.Entity<Patient.Model.Patient>(b =>
            {
                b.HasData(new Patient.Model.Patient
                {
                    Id = 5, Name = "Tijana", Surname = "Jovic", Ucid = 754621, Email = "tijana@gmail.com",
                    Password = "asd", BirthDate = new DateTime(1995, 4, 14),
                    UserRole = UserRole.Patient, Gender = Gender.Female, ChosenDoctorId = 1
                });
                b.OwnsOne(p => p.Address).HasData(new
                    { UserId = 5, Country = "Srbija", City = "Novi Sad", Street = "Kisacka", Number = 34 });
            });
            modelBuilder.Entity<Patient.Model.Patient>().HasOne(p => p.ChosenDoctor).WithMany(d => d.Patients)
                .HasForeignKey(p => p.ChosenDoctorId);
            modelBuilder.Entity<Blog.Model.Blog>().HasOne(b => b.Author).WithMany(d => d.WrittenBlogs);
            modelBuilder.Entity<Examination.Model.Examination>(b =>
            {
                b.HasOne(e => e.Doctor).WithMany(d => d.Examinations).HasForeignKey(e => e.DoctorId);
                b.HasOne(e => e.Patient).WithMany(p => p.Examinations).HasForeignKey(e => e.PatientId);
                b.HasOne(e => e.ExaminationReport).WithOne(er => er.Examination)
                    .HasForeignKey<ExaminationReport.Model.ExaminationReport>(er => er.ExaminationId);
            });
            modelBuilder.Entity<News.Model.News>().HasOne(n => n.Author).WithMany(d => d.News).HasForeignKey(n => n.AuthorId);
            modelBuilder.Entity<MedicalData.Model.MedicalData>().HasOne(md => md.Patient).WithMany(p => p.MedicalData)
                .HasForeignKey(md => md.PatientId);
            modelBuilder.Entity<MenstrualPeriod.Model.MenstrualPeriod>().HasOne(mp => mp.Patient).WithMany(p => p.MenstrualPeriods)
                .HasForeignKey(mp => mp.PatientId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
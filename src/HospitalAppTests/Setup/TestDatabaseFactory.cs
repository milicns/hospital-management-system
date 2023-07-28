using HospitalAPI;
using HospitalLibrary.Blog.Model;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.DoctorReferral.Model;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.ExaminationReport.Model;
using HospitalLibrary.MedicalData.Model;
using HospitalLibrary.MenstrualPeriod.Model;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.Settings;
using HospitalLibrary.SystemAdministrator.Model;
using HospitalLibrary.User.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAppTests.Setup;

public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            using var scope = BuildServiceProvider(services).CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<HospitalDbContext>();
            InitializeDatabase(db);
                   
        });
    }
    
    private static ServiceProvider BuildServiceProvider(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
        services.Remove(descriptor);

        services.AddDbContext<HospitalDbContext>(option => option.UseNpgsql(CreateConnectionStringForTest()));
        return services.BuildServiceProvider();
    }
    
    private static string CreateConnectionStringForTest()
    {
        return "Host=localhost;Database=HospitalDbTest;Username=postgres;Password=ftn;";
    }

    private static void InitializeDatabase(HospitalDbContext context)
    {
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        Address address1 = new Address { Country = "Srbija", City = "Novi Sad", Street = "Njegoseva", Number = 1 };
        Address address2 = new Address { Country = "Srbija", City = "Novi Sad", Street = "Strazilovska", Number = 1 };
        Address address3 = new Address { Country = "Srbija", City = "Novi Sad", Street = "Temerinska", Number = 121 };
        Patient patient1 = new Patient
        {
            Id = 1, Name = "Marko", Surname = "Markovic", Ucid = 7545213, Email = "marko@gmail.com",
            BirthDate = new DateTime(2000, 12, 12), Address = address2, Password = "asd", UserRole = UserRole.Patient,
            BloodType = BloodType.AbPositive,
            Blocked = true,
            
        };
        Patient patient2 = new Patient
        {
            Id = 2, Name = "Jelena", Surname = "Markovic", Ucid = 12321543, Email = "jelena@gmail.com",
            BirthDate = new DateTime(2000, 12, 12), Address = address3, Password = "asd", UserRole = UserRole.Patient,
            BloodType = BloodType.AbPositive, Gender = Gender.Female,
            Blocked = true,
        };
        Patient patient3 = new Patient
        {
            Id = 3, Name = "Vasilije", Surname = "Milic", Ucid = 1111543, Email = "milicvasilije9@gmail.com",
            BirthDate = new DateTime(2000, 12, 12), Address = address3, Password = "asd", UserRole = UserRole.Patient,
            BloodType = BloodType.AbPositive,
            Blocked = false,
        };
        Doctor doctor1 = new Doctor
        {
            Id = 4, Name = "Milos", Surname = "Petrovic", Address = address1, BirthDate = new DateTime(1993, 12, 12),
            Email = "milos@gmail.com", Password = "asd", Specialization = Specialization.Generalist, Ucid = 1234,
            UserRole = UserRole.Doctor, Patients = new List<Patient>()
        };
        SystemAdministrator systemAdministrator1 = new SystemAdministrator
        {
            Id = 5, Name = "Jovan", Surname = "Milic", Email = "jovan@gmail.com", Password = "asd",UserRole = UserRole.SystemAdministrator
        };
        patient1.ChosenDoctor = doctor1;
        patient2.ChosenDoctor = doctor1;
        patient3.ChosenDoctor = doctor1;
        Examination examination1 = new Examination
        {
            Id = 2, Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 5, 2, 8, 0, 0),
            State = ExaminationState.Scheduled
        };
        Examination examination2 = new Examination
        {
            Id = 3, Doctor = doctor1, Patient = patient3, ExaminationTerm = new DateTime(2023, 5, 15, 8, 0, 0),
            State = ExaminationState.Scheduled
        };
        Examination examination3 = new Examination
        {
            Id = 4, Doctor = doctor1, Patient = patient3, ExaminationTerm = new DateTime(2023, 5, 11, 8, 0, 0),
            State = ExaminationState.Scheduled
        };
        Examination examination4 = new Examination
        {
            Id = 5, Doctor = doctor1, Patient = patient2, ExaminationTerm = DateTime.Now.AddDays(-5),
            State = ExaminationState.Finished
        };
        ExaminationReport report1 = new ExaminationReport
        {
            Examination = examination4, Diagnosis = "Bacterial infection", Treatment = "Treatment1", Date = examination1.ExaminationTerm.AddHours(1)
        };
        examination4.ExaminationReportId = 2;
        DoctorReferral referral1 = new DoctorReferral
        {
            Id = 2, Patient = patient2, Doctor = doctor1, Specialization = Specialization.Cardiologist
        };
        MedicalData medicalData1 = new MedicalData
        {
            BloodSugar = 42, BodyFat = 15,
            MeasurementDate = DateTime.Now.AddDays(-1), BodyWeight = 80, BloodPressure = 125,
            PatientId = 1,
        };
        MenstrualPeriod menstrualPeriod1 = new MenstrualPeriod
        {
            PatientId = 2,Start = new DateTime(2021, 1, 1), End = new DateTime(2021, 1, 5)
        };
        Blog blog1 = new Blog
        {
            Title = "Title1", Content = "Content1", Author = doctor1
        };
        
        
        doctor1.Patients.Add(patient1);
        doctor1.Patients.Add(patient2);
        doctor1.Patients.Add(patient3);
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" DROP CONSTRAINT \"FK_Examinations_Users_DoctorId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" DROP CONSTRAINT \"FK_Examinations_Users_PatientId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" DROP CONSTRAINT \"FK_Examinations_DoctorReferrals_DoctorReferralId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"DoctorReferrals\" DROP CONSTRAINT \"FK_DoctorReferrals_Users_PatientId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"DoctorReferrals\" DROP CONSTRAINT \"FK_DoctorReferrals_Users_DoctorId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"ExaminationReports\" DROP CONSTRAINT \"FK_ExaminationReports_Examinations_ExaminationId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Blogs\" DROP CONSTRAINT \"FK_Blogs_Users_AuthorId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"News\" DROP CONSTRAINT \"FK_News_Users_AuthorId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"MenstrualPeriods\" DROP CONSTRAINT \"FK_MenstrualPeriods_Users_PatientId\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"MedicalData\" DROP CONSTRAINT \"FK_MedicalData_Users_PatientId\";");
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Users\";");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Blogs\" ADD CONSTRAINT \"FK_Blogs_Users_AuthorId\" FOREIGN KEY(\"AuthorId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"News\" ADD CONSTRAINT \"FK_News_Users_AuthorId\" FOREIGN KEY(\"AuthorId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"MedicalData\" ADD CONSTRAINT \"FK_MedicalData_Users_PatientId\" FOREIGN KEY(\"PatientId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"MenstrualPeriods\" ADD CONSTRAINT \"FK_MenstrualPeriods_Users_PatientId\" FOREIGN KEY(\"PatientId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"ExaminationReports\" ADD CONSTRAINT \"FK_ExaminationReports_Examinations_ExaminationId\" FOREIGN KEY(\"ExaminationId\") REFERENCES \"Examinations\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"DoctorReferrals\" ADD CONSTRAINT \"FK_DoctorReferrals_Users_PatientId\" FOREIGN KEY(\"PatientId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"DoctorReferrals\" ADD CONSTRAINT \"FK_DoctorReferrals_Users_DoctorId\" FOREIGN KEY(\"DoctorId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" ADD CONSTRAINT \"FK_Examinations_Users_DoctorId\" FOREIGN KEY(\"DoctorId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" ADD CONSTRAINT \"FK_Examinations_Users_PatientId\" FOREIGN KEY(\"PatientId\") REFERENCES \"Users\" (\"Id\")");
        context.Database.ExecuteSqlRaw("ALTER TABLE \"Examinations\" ADD CONSTRAINT \"FK_Examinations_DoctorReferrals_DoctorReferralId\" FOREIGN KEY(\"DoctorReferralId\") REFERENCES \"DoctorReferrals\" (\"Id\")");
        context.Users.Add(patient1);
        context.Users.Add(patient2);
        context.Users.Add(patient3);
        context.Users.Add(doctor1);
        context.Users.Add(systemAdministrator1);
        context.Examinations.Add(examination1);
        context.Examinations.Add(examination2);
        context.Examinations.Add(examination3);
        context.Examinations.Add(examination4);
        context.ExaminationReports.Add(report1);
        context.DoctorReferrals.Add(referral1);
        context.MedicalData.Add(medicalData1);
        context.MenstrualPeriods.Add(menstrualPeriod1);
        context.Blogs.Add(blog1);
        
        context.SaveChanges();
    }
}
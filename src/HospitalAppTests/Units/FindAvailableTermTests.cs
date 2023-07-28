
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.Doctor.Repository;
using HospitalLibrary.Doctor.Service;
using HospitalLibrary.DoctorReferral.Repository;
using HospitalLibrary.DoctorReferral.Service;
using HospitalLibrary.Examination.Dto;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Examination.Repository;
using HospitalLibrary.Examination.Service;
using HospitalLibrary.Patient.Model;
using Moq;
using Shouldly;


namespace HospitalAppTests.Units;

public class FindAvailableTermTests
{
    [Fact]
    public void Find_available_term_ideal_case_success()
    {
        IExaminationService examinationService = new ExaminationService(ExaminationRepositoryForSuccess(DoctorService()), DoctorService(), ReferralService());
        var scheduleExaminationDto = new SearchExaminationDto{DoctorId = 1, PatientId = 2, ExaminationTerm = new DateTime(2023, 2, 12), SearchPriority = SearchPriority.Doctor};
        var result = examinationService.RecommendAvailableTerm(scheduleExaminationDto);
        result.ExaminationTerm.Date.ShouldBe(new DateTime(2023, 2, 12));

    }

    [Fact]
    public void Find_available_term_doctor_priority_success()
    {
        IExaminationService examinationService = new ExaminationService(ExaminationRepositoryForSuccess(DoctorService()), DoctorService(), ReferralService());
        var scheduleExaminationDto = new SearchExaminationDto{DoctorId = 1, PatientId = 1, ExaminationTerm = new DateTime(2023, 2, 15), SearchPriority = SearchPriority.Doctor};
        var result = examinationService.RecommendAvailableTerm(scheduleExaminationDto);
        result.DoctorId.ShouldBe(1);

    }
    
    [Fact]
    public void Find_available_term_time_priority_success()
    {
        IExaminationService examinationService = new ExaminationService(ExaminationRepositoryForSuccess(DoctorService()), DoctorService(), ReferralService());
        var scheduleExaminationDto = new SearchExaminationDto{DoctorId = 1, PatientId = 1, ExaminationTerm = new DateTime(2023, 2, 15), SearchPriority = SearchPriority.Time,DoctorSpecialization = Specialization.Neurologist};
        var result = examinationService.RecommendAvailableTerm(scheduleExaminationDto);
        result.DoctorId.ShouldNotBe(1);

    }
    
    [Fact]
    public void Find_available_term_doctor_priority_failed()
    {
        IExaminationService examinationService = new ExaminationService(ExaminationRepositoryForDoctorPriorityFail(DoctorService()), DoctorService(), ReferralService());
        var scheduleExaminationDto = new SearchExaminationDto
        {
            DoctorId = 1, PatientId = 1, ExaminationTerm = new DateTime(2023, 2, 9),
            SearchPriority = SearchPriority.Doctor
        };
        var result = examinationService.RecommendAvailableTerm(scheduleExaminationDto);
        result.ShouldBeNull();
    }
    
    [Fact]
    public void Find_available_term_time_priority_failed()
    {
        IExaminationService examinationService = new ExaminationService(ExaminationRepositoryForTimePriorityFail(DoctorService()), DoctorService(), ReferralService());
        var scheduleExaminationDto = new SearchExaminationDto
        {
            DoctorId = 1, PatientId = 1, ExaminationTerm = new DateTime(2023, 2, 15),
            SearchPriority = SearchPriority.Time
        };
        var result = examinationService.RecommendAvailableTerm(scheduleExaminationDto);
        result.ShouldBeNull();
    }


    private IDoctorReferralService ReferralService()
    {
        var stubReferralRepository = new Mock<IDoctorReferralRepository>();
        var stubService = new Mock<DoctorReferralService>(stubReferralRepository.Object);
        return stubService.Object;
    }
    
    private IDoctorService DoctorService()
    {
        var stubDoctorRepository = new Mock<IDoctorRepository>();
        var stubService = new Mock<DoctorService>(stubDoctorRepository.Object);
        var doctors = new List<Doctor>();
        Doctor doctor1 = new Doctor{Id = 1, Name = "Marko", Surname = "Markovic", Specialization = Specialization.Neurologist};
        Doctor doctor2 = new Doctor { Id = 3, Name = "Stefan", Surname = "Markovic", Specialization = Specialization.Neurologist };
        doctors.Add(doctor1);
        doctors.Add(doctor2);
        stubDoctorRepository.Setup(m => m.GetAll()).Returns(doctors);
        return stubService.Object;
    }
    private IExaminationRepository ExaminationRepositoryForSuccess(IDoctorService doctorService)
    {
        var stubRepository = new Mock<IExaminationRepository>();
        var examinations = new List<Examination>();
        
        var doctors = doctorService.GetAll();

        Patient patient1 = new Patient{Id = 1, Name = "Milos", Surname = "Petrovic", BirthDate = new DateTime(1992, 12, 2)};
        Patient patient2 = new Patient{Id = 2, Name = "Jelena", Surname = "Tomic", BirthDate = new DateTime(1983, 12, 15)};
        Doctor doctor1 = doctors.ElementAt(0);
        Doctor doctor2 = doctors.ElementAt(1);
        
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 12, 8, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 8, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 10, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 12, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 14, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 16, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 18, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 10, 0, 0)});
        stubRepository.Setup(m => m.GetAll()).Returns(examinations);
        return stubRepository.Object;
    }
    
    private IExaminationRepository ExaminationRepositoryForDoctorPriorityFail(IDoctorService doctorService)
    {
        var stubRepository = new Mock<IExaminationRepository>();
        var examinations = new List<Examination>();
        var doctors = doctorService.GetAll();
        Doctor doctor1 = doctors.ElementAt(0);

        Patient patient1 = new Patient{Id = 1, Name = "Milos", Surname = "Petrovic", BirthDate = new DateTime(1992, 12, 2)};
        for (int i = 1; i <= 17; i++)
        {
            foreach (var term in FixedHospitalTerms.Terms)
            {
                examinations.Add(new Examination
                {
                    Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, i, term.Hours, term.Minutes, 0)
                });
            }
        }
        
        stubRepository.Setup(m => m.GetAll()).Returns(examinations);
        return stubRepository.Object;
    }
    
    
    private IExaminationRepository ExaminationRepositoryForTimePriorityFail(IDoctorService doctorService)
    {
        var stubRepository = new Mock<IExaminationRepository>();
        var examinations = new List<Examination>();
        var doctors = doctorService.GetAll();
        Doctor doctor1 = doctors.ElementAt(0);
        Doctor doctor2 = doctors.ElementAt(1);
        
        Patient patient1 = new Patient{Id = 1, Name = "Milos", Surname = "Petrovic", BirthDate = new DateTime(1992, 12, 2)};
        Patient patient2 = new Patient{Id = 2, Name = "Jelena", Surname = "Tomic", BirthDate = new DateTime(1983, 12, 15)};
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 8, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 10, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 12, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 14, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 16, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor1, Patient = patient1, ExaminationTerm = new DateTime(2023, 2, 15, 18, 0, 0)});
        
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 8, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 10, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 12, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 14, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 16, 0, 0)});
        examinations.Add(new Examination{Doctor = doctor2, Patient = patient2, ExaminationTerm = new DateTime(2023, 2, 15, 18, 0, 0)});
        
        
        stubRepository.Setup(m => m.GetAll()).Returns(examinations);
        return stubRepository.Object;
    }
}
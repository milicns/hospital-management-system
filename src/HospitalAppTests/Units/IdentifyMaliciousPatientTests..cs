
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.Patient.Repository;
using Moq;
using Shouldly;

namespace HospitalAppTests.Units;

public class IdentifyMaliciousPatientTests
{
    
    [Fact]
    public void Patient_malicious_success()
    {
        IPatientRepository patientRepository = PatientRepository();
        var patient = patientRepository.GetById(1);
        patient.IsMalicious().ShouldBeTrue();
    }
    
    [Fact]
    public void Patient_malicious_failed()
    {
        IPatientRepository patientRepository = PatientRepository();
        var patient = patientRepository.GetById(2);
        patient.IsMalicious().ShouldBeFalse();
        
    }
    
    private IPatientRepository PatientRepository()
    {
        var stubRepository = new Mock<IPatientRepository>();
        var examinations1 = new List<Examination>();
        var examinations2 = new List<Examination>();
        Patient patient1 = new Patient {Id = 1, Email = "asd", Password = "asd", BloodType = BloodType.AbNegative};
        Patient patient2 = new Patient {Id = 2, Email = "asd", Password = "asd", BloodType = BloodType.AbNegative};
        examinations1.Add(new Examination {Id = 1, PatientId = 1, DoctorId = 1,ExaminationTerm = DateTime.Now.AddDays(-12), State = ExaminationState.Canceled});
        examinations1.Add(new Examination {Id = 2, PatientId = 1, DoctorId = 1,ExaminationTerm = DateTime.Now.AddDays(-20), State = ExaminationState.Canceled});
        examinations1.Add(new Examination {Id = 3, PatientId = 1, DoctorId = 1,ExaminationTerm = DateTime.Now.AddDays(-5), State = ExaminationState.Canceled});
        examinations2.Add(new Examination {Id = 4, PatientId = 2, DoctorId = 1,ExaminationTerm = new DateTime(2023, 4, 11), State = ExaminationState.Canceled});
        examinations2.Add(new Examination {Id = 5, PatientId = 2, DoctorId = 1,ExaminationTerm = new DateTime(2023, 4, 2), State = ExaminationState.Canceled});
        patient1.Examinations = examinations1;
        patient2.Examinations = examinations2;
        stubRepository.Setup(m => m.GetById(1)).Returns(patient1);
        stubRepository.Setup(m => m.GetById(2)).Returns(patient2);
        return stubRepository.Object;
    }
}
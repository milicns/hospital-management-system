
using HospitalLibrary.Examination.Model;
using HospitalLibrary.ExaminationReport.Model;
using HospitalLibrary.MedicalData.Model;
using HospitalLibrary.MenstrualPeriod.Model;
using HospitalLibrary.Patient.Model;
using HospitalLibrary.Patient.Repository;
using HospitalLibrary.Patient.Service;
using HospitalLibrary.User.Model;
using HospitalLibrary.ValidationService;
using Moq;
using Shouldly;

namespace HospitalAppTests.Units;

public class PatientHealthConditionTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void Patient_is_healthy_for_blood_donating(int patientId, bool healthyFlag)
    {
        IPatientService patientService = new PatientService(RepositoryForPatientHealthCondition(),new Mock<IValidationService>().Object);
        var patient = patientService.GetById(patientId);
        patient.IsHealthy().ShouldBe(healthyFlag);
    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, true },
            new object[] { 2, false },
            new object[] { 3, false },
            new object[] { 4, false },
        };

    private IPatientRepository RepositoryForPatientHealthCondition()
    {
        var stubRepository = new Mock<IPatientRepository>();
        var medicalData = new MedicalData
        {
            Id = 1, PatientId = 1,  BloodPressure = 115, BodyFat = 12, BloodSugar = 12,BodyWeight = 12,MeasurementDate = DateTime.Now
        };
        var patient = new Patient
        {
            Id = 1, Email = "asd", Name = "asd",Password = "asd", BloodType = BloodType.AbNegative, MedicalData = new List<MedicalData>()
        };
        var medicalData2 = new MedicalData
        {
            Id = 2, PatientId = 2,  BloodPressure = 120, BodyFat = 28, BloodSugar = 12,BodyWeight = 12,MeasurementDate = DateTime.Now
        };
        var patient2 = new Patient
        {
            Id = 2, Email = "asd", Name = "asd",Password = "asd", BloodType = BloodType.AbNegative, MedicalData = new List<MedicalData>()
        };
        var patient3 = new Patient
        {
            Id = 3, Email = "asd", Name = "asd",Password = "asd", BloodType = BloodType.AbNegative, MedicalData = new List<MedicalData>(),
            MenstrualPeriods = new List<MenstrualPeriod>(), Gender = Gender.Female
        };
        var period1 = new MenstrualPeriod
        {
            PatientId = 3,Id = 1,Start = DateTime.Now,End = DateTime.Now.AddDays(5)
        };
        var patient4 = new Patient
        {
            Id = 4, Email = "asd", Name = "asd",Password = "asd", BloodType = BloodType.AbNegative, MedicalData = new List<MedicalData>(),
            Examinations = new List<Examination>(), Gender = Gender.Male
        };
        var examination = new Examination
        {
            Id = 1, PatientId = 4, State = ExaminationState.Finished, ExaminationTerm = DateTime.Now.AddDays(-11)
        };
        var examinationReport = new ExaminationReport
        {
            Id = 1, ExaminationId = 1, Diagnosis = "The patient is diagnosed with bacterial infection", Treatment = "asd"
        };
        examination.ExaminationReport = examinationReport;
        patient4.Examinations.Add(examination);
        patient.MedicalData.Add(medicalData);
        patient2.MedicalData.Add(medicalData2);
        patient3.MenstrualPeriods.Add(period1);
        stubRepository.Setup(m => m.GetById(1)).Returns(patient);
        stubRepository.Setup(m => m.GetById(2)).Returns(patient2);
        stubRepository.Setup(m => m.GetById(3)).Returns(patient3);
        stubRepository.Setup(m => m.GetById(4)).Returns(patient4);
        return stubRepository.Object;
    }
}
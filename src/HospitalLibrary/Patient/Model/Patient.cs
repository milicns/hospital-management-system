using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Examination.Model;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.User.Model;

namespace HospitalLibrary.Patient.Model;

public class Patient : User.Model.User
{
    public BloodType BloodType { get; set; }
    public int ChosenDoctorId { get; set; }
    public Doctor.Model.Doctor ChosenDoctor { get; set; }
    public List<MedicalData.Model.MedicalData> MedicalData { get; set; }
    public List<MenstrualPeriod.Model.MenstrualPeriod> MenstrualPeriods { get; set; }
    public List<Examination.Model.Examination> Examinations { get; set; }
    public bool Blocked { get; set; }
        
    public PatientDto ToPatientDto()
    {
        return new PatientDto
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Email = Email,
            Address = Address,
            BirthDate = BirthDate,
            BloodType = BloodType,
            Gender = Gender,
            Ucid = Ucid,
            ChosenDoctorId = ChosenDoctorId,
            Blocked = Blocked
        };
    }

    public bool IsMalicious()
    {
        var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            
        var canceledCount = Examinations
            .Count(e => e.State == ExaminationState.Canceled &&
                        e.ExaminationTerm >= thirtyDaysAgo);
            
        return canceledCount >= 3;
    }
    
    public bool IsHealthy()
    {
        var medicalData = GetMedicalDataFromLastTwoDays();
        return !(HasAbnormalBloodPressure(medicalData) || HasAboveNormalBodyFat(medicalData) || IsInMenstrualPeriod() || WasSickInLastTwoWeeks());
    }

    private IEnumerable<MedicalData.Model.MedicalData> GetMedicalDataFromLastTwoDays()
    {
        var now = DateTime.Now;
        var twoDaysAgo = now.AddDays(-2);
        return MedicalData.Where(d => d.MeasurementDate >= twoDaysAgo && d.MeasurementDate <= now);
    }

    private bool HasAbnormalBloodPressure(IEnumerable<MedicalData.Model.MedicalData> medicalData)
    {
        return medicalData.Any(d => d.BloodPressure < 90 || d.BloodPressure > 120);
    }

    private bool HasAboveNormalBodyFat(IEnumerable<MedicalData.Model.MedicalData> medicalData)
    {
        double bodyFatThreshold = Gender == Gender.Male ? 25 : 35;
        return medicalData.Any(d => d.BodyFat > bodyFatThreshold);
    }
    
    private bool IsInMenstrualPeriod()
    {
        if (Gender != Gender.Female)
            return false;
        
        var now = DateTime.Now;
        return MenstrualPeriods.Any(p => p.Start <= now && p.End >= now);
    }
    private bool WasSickInLastTwoWeeks()
    {
        if (Examinations == null)
            return false;
        return Examinations.Any(e => IsExaminationInLastTwoWeeks(e) && IsDiagnosisIndicatingSickness(e));
    }

    private bool IsExaminationInLastTwoWeeks(Examination.Model.Examination examination)
    {
        var now = DateTime.Now;
        var twoWeeksAgo = now.AddDays(-14);
        return examination.ExaminationTerm >= twoWeeksAgo && examination.ExaminationTerm <= now;
    }

    private bool IsDiagnosisIndicatingSickness(Examination.Model.Examination examination)
    {
        if (examination.ExaminationReport == null)
            return false;

        var diagnosis = examination.ExaminationReport.Diagnosis;
        return diagnosis.Contains("sick") || diagnosis.Contains("diagnosed") || diagnosis.Contains("ill") ||
               diagnosis.Contains("disease") || diagnosis.Contains("infection") || diagnosis.Contains("cold");
    }
}
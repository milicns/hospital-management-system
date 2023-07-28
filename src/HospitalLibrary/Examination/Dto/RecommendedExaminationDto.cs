using System;
using HospitalLibrary.Examination.Model;

namespace HospitalLibrary.Examination.Dto;

public class RecommendedExaminationDto
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime ExaminationTerm { get; set; }
    public int? DoctorReferralId { get; set; }
    
    public Model.Examination ToEntity()
    {
        return new Model.Examination
        {
            PatientId = PatientId,
            DoctorId = DoctorId,
            ExaminationTerm = ExaminationTerm,
            DoctorReferralId = DoctorReferralId,
            State = ExaminationState.Scheduled,
        };
    }
}
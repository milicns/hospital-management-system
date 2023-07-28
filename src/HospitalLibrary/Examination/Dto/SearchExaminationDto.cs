using System;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.Examination.Model;


namespace HospitalLibrary.Examination.Dto;

public class SearchExaminationDto
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public DateTime ExaminationTerm { get; set; }
    public SearchPriority SearchPriority { get; set; }
    public Specialization DoctorSpecialization { get; set; }

    public RecommendedExaminationDto ToRecommendedDto()
    {
        return new RecommendedExaminationDto
        {
            ExaminationTerm = ExaminationTerm,
            DoctorId = DoctorId,
            PatientId = PatientId
        };
    }
    
}
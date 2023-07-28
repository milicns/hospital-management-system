using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Examination.Dto;

namespace HospitalLibrary.Examination.Model;

public class Examination
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public Doctor.Model.Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    public Patient.Model.Patient Patient { get; set; }
    public DateTime ExaminationTerm { get; set; }
    public ExaminationState State { get; set; }
    
    public int? ExaminationReportId { get; set; }
    public ExaminationReport.Model.ExaminationReport ExaminationReport { get; set; }
    
    [ForeignKey("DoctorReferralId")]
    public int? DoctorReferralId { get; set; }
    public DoctorReferral.Model.DoctorReferral DoctorReferral { get; set; }
    
    public ExaminationDto ToDto()
    {
       
        return new ExaminationDto
        {
            Id = Id,
            DoctorId = DoctorId,
            PatientId = PatientId,
            DoctorReferralId = DoctorReferralId,
            Date = ExaminationTerm,
            ExaminationReportId = ExaminationReportId,
            State = State
        };
    }

}
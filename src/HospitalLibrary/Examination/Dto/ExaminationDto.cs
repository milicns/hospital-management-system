using System;
using HospitalLibrary.Examination.Model;


namespace HospitalLibrary.Examination.Dto;

public class ExaminationDto
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int? DoctorReferralId { get; set; }
    public int? ExaminationReportId { get; set; }
    public DateTime Date { get; set; }
    public ExaminationState State { get; set; }
}
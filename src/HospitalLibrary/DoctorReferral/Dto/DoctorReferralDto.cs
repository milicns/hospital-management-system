using HospitalLibrary.Doctor.Model;

namespace HospitalLibrary.DoctorReferral.Dto;

public class DoctorReferralDto
{
    public int Id { get; set; } 
    public Specialization Specialization { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public bool Used { get; set; }
}
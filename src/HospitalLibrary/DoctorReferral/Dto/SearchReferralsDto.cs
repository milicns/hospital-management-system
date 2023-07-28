using HospitalLibrary.Doctor.Model;

namespace HospitalLibrary.DoctorReferral.Dto;

public class SearchReferralsDto
{
    public int PatientId { get; set; }
    public Specialization Specialization { get; set; }
}
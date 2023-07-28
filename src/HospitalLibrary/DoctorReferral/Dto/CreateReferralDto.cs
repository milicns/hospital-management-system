using HospitalLibrary.Doctor.Model;

namespace HospitalLibrary.DoctorReferral.Dto;

public class CreateReferralDto
{
    public Specialization Specialization { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public bool Used { get; set; }
    
    public Model.DoctorReferral ToEntity()
    {
        return new Model.DoctorReferral
        {
            PatientId = PatientId,
            DoctorId = DoctorId,
            Specialization = Specialization,
            Used = Used
        };
    }
}
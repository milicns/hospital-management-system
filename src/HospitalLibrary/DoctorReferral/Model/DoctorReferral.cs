using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalLibrary.Doctor.Model;
using HospitalLibrary.DoctorReferral.Dto;

namespace HospitalLibrary.DoctorReferral.Model;

public class DoctorReferral
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
    public int Id { get; set; } 
    public Specialization Specialization { get; set; }
    public int PatientId { get; set; }
    [ForeignKey("PatientId")]
    public Patient.Model.Patient Patient { get; set; }
    public int DoctorId { get; set; }
    [ForeignKey("DoctorId")]
    public Doctor.Model.Doctor Doctor { get; set; }
    public bool Used { get; set; }
    
    public DoctorReferralDto ToDto()
    {
        return new DoctorReferralDto
        {
            Id = Id,
            Specialization = Specialization,
            PatientId = PatientId,
            DoctorId = DoctorId,
            Used = Used
        };
    }
}
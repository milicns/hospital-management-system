using System.Collections.Generic;
using HospitalLibrary.DoctorReferral.Dto;

namespace HospitalLibrary.DoctorReferral.Service;

public interface IDoctorReferralService
{
    DoctorReferralDto Create(CreateReferralDto createReferralDto);
    IEnumerable<DoctorReferralDto> PatientNotUsedReferrals(int patientId);
    Model.DoctorReferral GetById(int id);
}
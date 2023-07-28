using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.DoctorReferral.Dto;
using HospitalLibrary.DoctorReferral.Repository;

namespace HospitalLibrary.DoctorReferral.Service;

public class DoctorReferralService : IDoctorReferralService
{
    private readonly IDoctorReferralRepository _doctorReferralRepository;
    
    public DoctorReferralService(IDoctorReferralRepository doctorReferralRepository)
    {
        _doctorReferralRepository = doctorReferralRepository;
    }
    
    public DoctorReferralDto Create(CreateReferralDto createReferralDto)
    {
        return _doctorReferralRepository.Create(createReferralDto.ToEntity()).ToDto();
    }

    public Model.DoctorReferral GetById(int id)
    {
        return _doctorReferralRepository.GetById(id);
    }
    
    public IEnumerable<DoctorReferralDto> PatientNotUsedReferrals(int patientId)
    {
        return _doctorReferralRepository.PatientNotUsedReferrals(patientId).Select(dr => dr.ToDto());
    }
    
}
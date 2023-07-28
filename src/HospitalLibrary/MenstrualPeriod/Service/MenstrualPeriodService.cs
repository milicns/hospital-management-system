using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.MenstrualPeriod.Dto;
using HospitalLibrary.MenstrualPeriod.Repository;
using HospitalLibrary.Patient.Service;
using HospitalLibrary.User.Model;

namespace HospitalLibrary.MenstrualPeriod.Service;


public class MenstrualPeriodService : IMenstrualPeriodService
{
    private readonly IMenstrualPeriodRepository _menstrualPeriodRepository;
    private readonly IPatientService _patientService;
    
    public MenstrualPeriodService(IMenstrualPeriodRepository menstrualPeriodRepository, IPatientService patientService)
    {
        _menstrualPeriodRepository = menstrualPeriodRepository;
        _patientService = patientService;
    }

    public MenstrualPeriodDto SaveMenstrualPeriod(MenstrualPeriodDto periodDto)
    {
        var patient = _patientService.GetById(periodDto.PatientId);
        if (patient.Gender is not Gender.Female) return null;
        return _menstrualPeriodRepository.Create(periodDto.ToEntity()).ToDto();
    }
    
    public IEnumerable<MenstrualPeriodDto> GetPatientMenstrualPeriods(int patientId)
    {
        return _menstrualPeriodRepository.GetPatientMenstrualPeriods(patientId).Select(mp=>mp.ToDto());
    }
}
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.MedicalData.Repository;

namespace HospitalLibrary.MedicalData.Service;

public class MedicalDataService : IMedicalDataService
{
    private readonly  IMedicalDataRepository _medicalDataRepository;

    public MedicalDataService(IMedicalDataRepository medicalDataRepository)
    {
        _medicalDataRepository = medicalDataRepository;
    }

    public MeasuredDataDto Measure(MeasuredDataDto measuredDataDto)
    {
        return _medicalDataRepository.Create(measuredDataDto.ToEntity()).ToDto();
    }
    
    public IEnumerable<MeasuredDataDto> GetPatientMeasurementsRecord(int patientId)
    {
        return _medicalDataRepository.GetPatientMeasurementsRecord(patientId).Select(md => md.ToDto());
    }
}
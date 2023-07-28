using System.Collections.Generic;
using HospitalLibrary.MedicalData.Dto;

namespace HospitalLibrary.MedicalData.Service;

public interface IMedicalDataService
{
    MeasuredDataDto Measure(MeasuredDataDto measuredDataDto);
    IEnumerable<MeasuredDataDto> GetPatientMeasurementsRecord(int patientId);
}
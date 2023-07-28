using System.Collections.Generic;
using HospitalLibrary.MenstrualPeriod.Dto;

namespace HospitalLibrary.MenstrualPeriod.Service;

public interface IMenstrualPeriodService
{
    IEnumerable<MenstrualPeriodDto> GetPatientMenstrualPeriods(int patientId);
    MenstrualPeriodDto SaveMenstrualPeriod(MenstrualPeriodDto periodDto);
}
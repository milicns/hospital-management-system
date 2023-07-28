using System.Collections.Generic;
namespace HospitalLibrary.MenstrualPeriod.Repository;


public interface IMenstrualPeriodRepository
{
    Model.MenstrualPeriod Create(Model.MenstrualPeriod menstrualPeriod);
    IEnumerable<Model.MenstrualPeriod> GetPatientMenstrualPeriods(int patientId);
}
using System.Collections.Generic;

namespace HospitalLibrary.MedicalData.Repository;

public interface IMedicalDataRepository
{
    Model.MedicalData Create(Model.MedicalData medicalData);
    IEnumerable<Model.MedicalData> GetPatientMeasurementsRecord(int patientId);
}
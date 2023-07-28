
using System.Collections.Generic;

namespace HospitalLibrary.Patient.Repository
{
    public interface IPatientRepository
    {
        Model.Patient Create(Model.Patient patient);
        Model.Patient GetById(int id);
        void Update(Model.Patient patient);
        IEnumerable<Model.Patient> MaliciousPatients();
        IEnumerable<Model.Patient> BlockedPatients();
        IEnumerable<Model.Patient> HealthyPatients();
        IEnumerable<Model.Patient> GetDoctorsListOfPatients(int doctorId);

    }
}
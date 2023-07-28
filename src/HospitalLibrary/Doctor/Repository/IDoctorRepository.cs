using System.Collections;
using System.Collections.Generic;

namespace HospitalLibrary.Doctor.Repository
{
    public interface IDoctorRepository
    {
        Model.Doctor GetById(int id);
        void Update(Model.Doctor doctor);
        IEnumerable<Model.Doctor> GetAll();
        IEnumerable<Model.Doctor> GetGeneralPractitioners();
        Model.Doctor GetChosenDoctorForPatient(int patientId);
        
    }
}
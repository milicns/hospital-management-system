using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.Doctor.Dto;
using HospitalLibrary.Patient.Dto;

namespace HospitalLibrary.Doctor.Service
{
    public interface IDoctorService
    {
        IEnumerable<DoctorDto> GetGeneralPractitioners();
        Model.Doctor GetDoctorById(int id);
        IEnumerable<Model.Doctor> GetAll();
        DoctorDto GetChosenDoctorForPatient(int patientId);
    }
}
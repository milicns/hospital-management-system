using System.Collections;
using System.Collections.Generic;
using HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.Patient.Dto;

namespace HospitalLibrary.Patient.Service
{
    public interface IPatientService
    {
        Model.Patient GetById(int id);
        PatientDto RegisterPatient(CreatePatientDto createPatientDto);
        PatientDto Block(int id);
        PatientDto Unblock(int id);
        IEnumerable<PatientDto> MaliciousPatients();
        IEnumerable<PatientDto> BlockedPatients();
        IEnumerable<PatientDto> HealthyPatients();
        IEnumerable<PatientDto> GetDoctorsListOfPatients(int doctorId);

    }
}
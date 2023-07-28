using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HospitalLibrary.MedicalData.Dto;
using HospitalLibrary.Patient.Dto;
using HospitalLibrary.Patient.Repository;
using HospitalLibrary.Patient.Service;
using HospitalLibrary.ValidationService;

namespace HospitalLibrary.Patient.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IValidationService _validationService;
        

        public PatientService(IPatientRepository patientRepository, IValidationService validationService)
        {
            _patientRepository = patientRepository;
            _validationService = validationService;
        }
        

        public PatientDto RegisterPatient(CreatePatientDto createPatientDto)
        {
            _validationService.ValidateUniqueness(createPatientDto);
            return _patientRepository.Create(createPatientDto.ToEntity()).ToPatientDto();
        }

        public PatientDto Block(int id)
        {
            Patient.Model.Patient patient = GetById(id);
            patient.Blocked = true;
            Update(patient);
            return patient.ToPatientDto();
        }
        
        public PatientDto Unblock(int id)
        {
            Model.Patient patient = GetById(id);
            patient.Blocked = false;
            Update(patient);
            return patient.ToPatientDto();
        }
        
        public IEnumerable<PatientDto> HealthyPatients()
        {
            return _patientRepository.HealthyPatients().Select(p => p.ToPatientDto());
        }
        
        public IEnumerable<PatientDto> MaliciousPatients()
        {
            return _patientRepository.MaliciousPatients().Select(p => p.ToPatientDto());
        }

        public IEnumerable<PatientDto> BlockedPatients()
        {
            return _patientRepository.BlockedPatients().Select(p => p.ToPatientDto());
        }
        
        public Model.Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        private void Update(Model.Patient patient)
        {
            _patientRepository.Update(patient);
        }
        
        public IEnumerable<PatientDto> GetDoctorsListOfPatients(int doctorId)
        {
            return _patientRepository.GetDoctorsListOfPatients(doctorId).Select(p => p.ToPatientDto());
        }
        
    }
}
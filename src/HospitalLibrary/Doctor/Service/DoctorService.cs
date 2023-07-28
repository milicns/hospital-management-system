using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Doctor.Dto;
using HospitalLibrary.Doctor.Repository;
using HospitalLibrary.Patient.Dto;

namespace HospitalLibrary.Doctor.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<DoctorDto> GetGeneralPractitioners()
        {
            return _doctorRepository.GetGeneralPractitioners().Select(d => d.ToDto());
        }
        
        public Model.Doctor GetDoctorById(int id)
        {
            return _doctorRepository.GetById(id);
        }

        public IEnumerable<Model.Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public DoctorDto GetChosenDoctorForPatient(int patientId)
        {
            return _doctorRepository.GetChosenDoctorForPatient(patientId).ToDto();
        }
        
        
    }
}
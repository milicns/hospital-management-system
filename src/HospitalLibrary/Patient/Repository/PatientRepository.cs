using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Patient.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public Model.Patient Create(Model.Patient patient)
        {
            var createdPatient = _context.Patients.Add(patient);
            _context.SaveChanges();
            return createdPatient.Entity;
        }
        
        public Model.Patient GetById(int id)
        {
            return _context.Patients.Find(id);
        }

        public IEnumerable<Model.Patient> MaliciousPatients()
        {
            return _context.Patients.Include(p => p.Examinations).AsEnumerable().Where(p => p.IsMalicious() && !p.Blocked);
        }
        
        public IEnumerable<Model.Patient> HealthyPatients()
        {
            return _context.Patients.Include(p=>p.Examinations).ThenInclude(e=>e.ExaminationReport)
                .Include(p=>p.MedicalData).Include(p=>p.MenstrualPeriods)
                .AsEnumerable().Where(p => p.IsHealthy());
        }

        public IEnumerable<Model.Patient> BlockedPatients()
        {
            return _context.Patients.Where(p => p.Blocked);
        }

        public void Update(Model.Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        
        public IEnumerable<Model.Patient> GetDoctorsListOfPatients(int doctorId)
        {
            return _context.Patients.Where(p => p.ChosenDoctorId == doctorId);
        }

    }
}